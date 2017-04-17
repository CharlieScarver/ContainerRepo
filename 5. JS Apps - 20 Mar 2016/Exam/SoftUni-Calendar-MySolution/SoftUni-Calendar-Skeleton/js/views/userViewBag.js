var app = app || {};

app.userViewBag = (function (){
    function showLoginPage(menuSelector, mainSelector) {
        $.get('templates/menu-login.html', function (template) {
            $(menuSelector).html(template);
        });

        $.get('templates/login.html', function (template) {
           $(mainSelector).html(template);

            $('#login-button').on('click', function () {
                var username = $('#username').val(),
                    password = $('#password').val();

                Sammy(function () {
                    this.trigger('login',
                        {
                            username: username,
                            password: password
                        });
                });
            });
        });
    }

    function showRegisterPage(menuSelector, mainSelector) {
        $.get('templates/menu-login.html', function (template) {
            $(menuSelector).html(template);
        });

        $.get('templates/register.html', function (template) {
            $(mainSelector).html(template);

            $('#register-button').on('click', function () {
                var username = $('#username').val(),
                    password = $('#password').val(),
                    confirmPass = $('#confirm-password').val();

                if (password === confirmPass) {
                    Sammy(function () {
                        this.trigger('register',
                            {
                                username: username,
                                password: password
                            });
                    });
                } else {
                    noty({
                        theme: 'relax',
                        text: 'The passwords do not match!',
                        type:'error',
                        timeout: 2000,
                        closeWith: ['click']
                    });
                }
            });
        });
    }

    return {
        load: function () {
            return {
                showLoginPage: showLoginPage,
                showRegisterPage: showRegisterPage
            };
        }
    };
}());