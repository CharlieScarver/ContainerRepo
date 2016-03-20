var app = app || {};

app.userViewBag = (function (){
    function showLoginPage(selector) {
        $.get('templates/login.html', function (template) {
           $(selector).html(template);

            $('#loginButton').on('click', function () {
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

    function showRegisterPage(selector) {
        $.get('templates/register.html', function (template) {
            $(selector).html(template);

            $('#registerButton').on('click', function () {
                var username = $('#username').val(),
                    password = $('#password').val(),
                    fullName = $('#fullName').val();

                Sammy(function () {
                    this.trigger('register',
                        {
                            username: username,
                            password: password,
                            fullName: fullName
                        });
                });
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