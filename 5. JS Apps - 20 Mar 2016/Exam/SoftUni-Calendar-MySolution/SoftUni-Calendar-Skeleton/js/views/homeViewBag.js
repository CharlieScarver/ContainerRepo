var app = app || {};

app.homeViewBag = (function (){
    function showWelcomeGuest(menuSelector, mainSelector) {
        $.get('templates/menu-login.html', function (template) {
            $(menuSelector).html(template);
        });

        $.get('templates/welcome-guest.html', function (template) {
            $(mainSelector).html(template);
        });
    }

    function showWelcomeUser(menuSelector, mainSelector, data) {
        $.get('templates/menu-home.html', function (template) {
            $(menuSelector).html(template);
        });

        $.get('templates/welcome-user.html', function (template) {
            var renderedHtml = Mustache.render(template, data);
            $(mainSelector).html(renderedHtml);
        });
    }

    return {
        load: function () {
            return {
                showWelcomeGuest: showWelcomeGuest,
                showWelcomeUser: showWelcomeUser
            };
        }
    };
}());