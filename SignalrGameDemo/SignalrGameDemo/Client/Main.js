

var Game = function() {
    var me = this;

    var started = false,
        player = null,
        allPlayers = [],
        bullets = [];
    
    // Declare a proxy reference to the server hub
    var gameHub = $.connection.gameHub;

    // Throttle the fire button
    var _throttledFire = _.throttle(gameHub.server.fire, 500);
    
    // Handle key input
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
    
    // Setup functions for server to call
    gameHub.client.hello = function(message) {
        toastr.info(message);
    };

    gameHub.client.connect = function(thisPlayer) {
        player = thisPlayer;
       
    };

    gameHub.client.updateGame = function(gameState) {
        allPlayers = gameState.Players;
        bullets = gameState.Bullets;
        me.update();
        me.draw();
    };

    var drawPlayer = function(p) {
        $("#game").append("<div class=\"tank player" + p.ConnectionId + " \"></div>");
        $(".player" + p.ConnectionId).css({ top: p.Y + 'px', left: p.X + 'px', transform: 'rotate(' + p.Dir + 'rad)' });
    };

    var drawBullet = function(b) {
        $("#game").append("<div class=\"bullet bullet" + b.Id + " \"></div>");
        $(".bullet" + b.Id).css({ top: b.Y + 'px', left: b.X + 'px', transform: 'rotate(' + b.Dir + 'rad)' });
    };
    
    

    me.callServer = gameHub.server.hello;

    

    me.start = function(connectionReady) {
        if (!started) {
            started = true;
            $.connection.hub.start().done(function() {
                toastr.info("Yo Dawg we are playing tanks!");
                mainloop();
            }).fail(function() {
                toastr.error("Yo Dawg something is wrong");
            });
        }
    };

    me.update = function() {
        onKeyPress();
    };

    me.draw = function () {
        $("#game").html("");
        
        // Draw Players
        var currentPlayer = allPlayers.filter(function (p) {
            return p.ConnectionId == player.ConnectionId;
        })[0];

        drawPlayer(currentPlayer);
        
        var others = allPlayers.filter(function(p) {
            return p.ConnectionId != player.ConnectionId;
        });
        others.forEach(function (p) {
            drawPlayer(p);
        });
        
        // Draw bullets
        bullets.forEach(function (b) {
            drawBullet(b);
        });        


    };
    
    

    var mainloop = function() {
        // loop every frame update and draw
        toastr.info("Mainloop Started");

        
    };

    return me;
};


var game = new Game();


game.start();

