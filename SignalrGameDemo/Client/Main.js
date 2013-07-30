

var Game = function () {
    var me = this;

    // private vars
    var started = false,
        connected = false,
        player = null,
        allPlayers = [],
        bullets = [];
    
    // Declare a proxy reference to the server hub
    var gameHub = $.connection.gameHub;

    // Throttle the fire button
    var _throttledFire = _.throttle(gameHub.server.fire, 500);
    

    //#region Input
    // Capture Key Events
    var keys = [];
    window.onkeydown = function (ev) {
        if (keys.indexOf(ev.keyCode) < 0) {
            keys.push(ev.keyCode);
        }
    };
    window.onkeyup = function (ev) {
        var key = keys.indexOf(ev.keyCode);
        keys.splice(key, 1);
    };
    var onKeyPress = function () {

        //uparrow || w
        if (keys.indexOf(38) > -1 || keys.indexOf(87) > -1) {
            gameHub.server.move("Forward");
        }
        //downarrow || s
        if (keys.indexOf(40) > -1 || keys.indexOf(83) > -1) {
            gameHub.server.move("Backward");
        }
        //leftarrow || a
        if (keys.indexOf(37) > -1 || keys.indexOf(65) > -1) {
            gameHub.server.rotate("TurnLeft");
        }
        //rightarrow || d
        if (keys.indexOf(39) > -1 || keys.indexOf(68) > -1) {
            gameHub.server.rotate("TurnRight");
        }
        // spacebar
        if (keys.indexOf(32) > -1) {
            _throttledFire();
        }
    };
    //#endregion

    //#region SignalR 
    // Setup functions for server to call
    gameHub.client.hello = function (message) {
        toastr.info(message);
    };

    gameHub.client.connect = function (thisPlayer) {
        player = thisPlayer;
        connected = true;

    };

    gameHub.client.updateGame = function (gameState) {
        allPlayers = gameState.Players;
        bullets = gameState.Bullets;
        if (started && connected) {
            me.update();
            me.draw();
        }
    };
    //#endregion

    //#region Drawing 
    var drawPlayer = function (p, isEnemy) {
        var $player = $(".player" + p.ConnectionId);
        if (!$player.length) {
            $("#game").append("<div class=\"tank "+ (isEnemy?"badtank":"")+" player" + p.ConnectionId + " \"></div>");
        }
        $player.css({ top: p.Pos.Y + 'px', left: p.Pos.X + 'px', transform: 'rotate(' + p.Dir + 'rad)' });
    };

    var erasePlayer = function(p) {
        var $player = $(".player" + p.ConnectionId);
        $player.remove();
    };

    var drawBullet = function (b) {
        var $bullet = $(".bullet" + b.Id);
        if (!$bullet.length) {
            $("#game").append("<div class=\"bullet bullet" + b.Id + " \"></div>");
        }
        $bullet.css({ top: b.Pos.Y + 'px', left: b.Pos.X + 'px', transform: 'rotate(' + b.Dir + 'rad)' });
    };
    var eraseBullet = function(b) {
        var $bullet = $(".bullet" + b.Id);
        $bullet.remove();
    };
    
    var healthColor = function (life) {
        if (life < 30) {
            return 'red';
        } else if (life < 50) {
            return 'orange';
        } else if (life < 70) {
            return 'yellow';
        } else if (life < 90) {
            return 'greenyellow';
        } else {
            return 'green';
        }
    };
    //#endregion
    
    //#region MainLoop
    me.update = function () {
        var currentPlayer = allPlayers.filter(function (p) {
            return p.ConnectionId == player.ConnectionId;
        })[0];

        if (currentPlayer.Life < 0) {
            alert("You Died!");
            window.location = window.location;
        }
        onKeyPress();
    };
   
   

    

    me.draw = function () {
        var others = allPlayers.filter(function (p) {
            return p.ConnectionId != player.ConnectionId;
        });
        others.forEach(function (p) {
            drawPlayer(p, true);
        });

        // Draw Players
        var currentPlayer = allPlayers.filter(function (p) {
            return p.ConnectionId == player.ConnectionId;
        })[0];

        drawPlayer(currentPlayer);
        

        $("#healthbar").css({ width: (currentPlayer.Life / 100 * window.innerWidth) + 'px', backgroundColor: healthColor(currentPlayer.Life) });



        // Draw bullets
        bullets.forEach(function (b) {
            drawBullet(b);
        });
        
        // Remove dead players
        others.filter(function(p) {
            return p.Life < 0;
        }).forEach(function(p) {
            erasePlayer(p);
        });
        
        // Remove dead bullets
        bullets.filter(function(b) {
            return b.Life <= 0;
        }).forEach(function(b) {
            eraseBullet(b);
        });
    };
    //#endregion

    me.start = function () {
        $("#game").css({ width: window.innerWidth + 'px', height: window.innerHeight - 20 + 'px' });
        $("#healthbar").css({ width: window.innerWidth + 'px' });
        if (!started) {
            started = true;
            $.connection.hub.start().done(function () {
                toastr.info("Yo Dawg we are playing tanks!");
                $(window).resize(function () {
                    $("#game").css({ width: window.innerWidth + 'px', height: window.innerHeight - 20 + 'px' });
                    $("#healthbar").css({ width: window.innerWidth + 'px' });
                });

            }).fail(function () {
                toastr.error("Yo Dawg something is wrong");
            });
        }
    };


    return me;
};


var game = new Game();


game.start();

