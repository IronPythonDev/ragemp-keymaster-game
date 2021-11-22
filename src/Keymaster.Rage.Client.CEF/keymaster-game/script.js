let moving = {};

let gameSuccess = null;

let settings = {
  handleEnd: true,
  speed: 2,
  baseSpeed: 2,
  scoreWin: 50,
  scoreLose: -50,
  maxTime: 30000,
  maxMistake: 5,
  speedIncrement: 1,
};
let score = 0;
let mistake = 0;

let keycontainer = $("#keymaster-keycontainer");

let game;

let keys = [
  {
    key: "g",
    right: -128,
    controlled: true,
    shown: true,
  },
  {
    key: "e",
    right: -128,
    controlled: false,
    shown: false,
  },
  {
    key: "e",
    right: -128,
    controlled: false,
    shown: false,
  },
  {
    key: "r",
    right: -128,
    controlled: false,
    shown: false,
  },
  {
    key: "g",
    right: -128,
    controlled: false,
    shown: false,
  },
  {
    key: "e",
    right: -128,
    controlled: false,
    shown: false,
  },
  {
    key: "t",
    right: -128,
    controlled: false,
    shown: false,
  },
  {
    key: "t",
    right: -128,
    controlled: false,
    shown: false,
  },
];

const openModal = (title, body) => {
  if (title) $("#keymaster-info-title").html(title);
  if (body) $("#keymaster-info-body").html(body);

  $("#keymaster-info").modal("show");
};

const showKeymaster = () => {
  let promise = new Promise((resolve, reject) => {
    score = 0;
    $("#keymaster-score").html("0");
    $("#keymaster-timer").html("0s");
    $("#keymaster-field").fadeIn(300, () => {
      let countdown = 3;
      $("#keymaster-overlay").show(0);
      let cd = window.setInterval(() => {
        if (countdown == -1) {
          clearInterval(cd);
          $("#keymaster-overlay").hide(0);
          resolve(true);
        } else if (countdown == 0) {
          $("#keymaster-countdown").html("START");
          countdown--;
        } else $("#keymaster-countdown").html(countdown--);
      }, 1000);
    });
  });

  return promise;
};
const startGame = () => {
  showKeymaster().then(() => {
    moveKey();
    drawKeys();
    initKeymaster();
  });
};

const generateKeys = (keylist) => {
  if (keylist.length > 0) {
    keys = [];
    for (let i = 0; i < keylist.length; i++) {
      keys.push({
        key: keylist[i],
        right: -128,
        shown: i === 0 ? true : false,
      });
    }
  }
};
const endGame = (status, message) => {
  clearInterval(game);
  settings.speed = settings.baseSpeed;

  mistake = 0;

  if (status) gameSuccess = true;
  else gameSuccess = false;

  $("#keymaster-field").fadeOut(500);
  if (settings.handleEnd) {
    openModal(status ? "Completed!" : "Failed!", message);
  } else {
    sendResult();
  }
};

const nextKey = async (i) => {
  if (i >= keys.length) i = 0;
  $("#keymaster-point-char").html(keys[i].key);
  keys[i].shown = true;
  $(`[data-kid='${i}']`)
    .css("right", "-128px")
    .css("background-color", "#eee")
    .show(200);
  keys[i].right = -128;
};

const evalStatus = async () => {
  if (score >= settings.scoreWin) {
    endGame(true, "Win");
    return;
  }
  if (score <= settings.scoreLose) {
    endGame(false, "You went into the negative!");
    return;
  }
  if (mistake > settings.maxMistake) {
    endGame(false, `Made more than ${settings.maxMistake} mistakes.`);
    return;
  }
};

const evalKey = async (key) => {
  for (let i = 0; i < keys.length; i++) {
    if (keys[i].key == key && keys[i].shown) {
      if (
        keys[i].right + 128 <= settings.boxL &&
        keys[i].right + 128 >= settings.boxR
      ) {
        settings.speed += settings.speedIncrement;
        keys[i].shown = false;
        updateScore(keys[i].right + 128 - settings.boxR);
        await animateKeypress(i, true);
        evalStatus();
        nextKey(i + 1);
        return;
      } else {
        settings.speed = settings.baseSpeed;
        keys[i].shown = false;
        updateScore(-50);
        mistake++;
        await animateKeypress(i, false);
        evalStatus();
        nextKey(i + 1);
        // startGame();
        return;
      }
    }
  }
};

const animateKeypress = async (kid, success) => {
  if (success) {
    $("[data-kid='" + kid + "']")
      .css("background-color", "#99e7ab")
      .hide(300);
  } else {
    $("[data-kid='" + kid + "']")
      .css("background-color", "#ed99a1")
      .hide(300);
  }
};

const updateScore = async (s) => {
  score += s;
  $("#keymaster-score").html(Math.ceil(score));
};

const initKeymaster = () => {
  settings.handleEnd = true;

  settings.time = 0;

  settings.width = window.innerWidth;
  settings.height = window.innerHeight;

  let element = document.getElementById("keymaster-point");
  settings.boxR = Math.floor(
    settings.width - element.getBoundingClientRect().right
  );
  settings.boxL = Math.floor(
    settings.width - element.getBoundingClientRect().left
  );
};

const drawKeys = async () => {
  keycontainer.html(
    `<div id="keymaster-point"><span id="keymaster-point-char"></span></div>`
  );
  for (let i = 0; i < keys.length; i++) {
    if (keys[i].shown) {
      keycontainer.append(
        `<div class="keymaster-key" data-kid="${i}" style="right:${keys[i].right}px"><span>${keys[i].key}</span></div>`
      );
      $("#keymaster-point-char").html(keys[i].key);
    } else
      keycontainer.append(
        `<div class="keymaster-key" data-kid="${i}"style="display:none"><span>${keys[i].key}</span></div>`
      );
  }
};

const moveKey = async () => {
  game = window.setInterval(() => {
    for (let i = 0; i < keys.length; i++) {
      if (keys[i].shown) {
        $("[data-kid='" + i + "']").css("right", `+=${settings.speed}px`);
        keys[i].right += settings.speed;

        if (keys[i].right >= settings.boxR + 128) {
          keys[i].shown = false;
          updateScore(-50);
          animateKeypress(i, false);
          nextKey(i + 1);
          settings.speed = settings.baseSpeed;
          mistake++;
          evalStatus();
        }
      }
    }
    settings.time += 10;
    $("#keymaster-timer").html((settings.time / 1000).toFixed(1) + "s");
    if (settings.time >= settings.maxTime) {
      endGame(false, "Time ran out!");
    }
  }, 10);
};

const sendResult = () => {
  if (mp != null) {
    mp.trigger("Browser:Close", window.browserId);
    mp.trigger("Client:Game:KeyMaster:SendScoreToServer", score);
  }
};

$(window).keydown((event) => {
  evalKey(event.key);
});

$("#keymaster-info").on("hide.bs.modal", () => {
  if (gameSuccess !== null) {
    sendResult();
  }
});

startGame();
