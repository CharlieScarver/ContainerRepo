var app = app || {};

app.lecturesViewBag = (function(){
    function showAllLectures(menuSelector, mainSelector, data) {
        $.get('templates/menu-home.html', function (template) {
            $(menuSelector).html(template);
        });

        $.get('templates/calendar.html', function (template) {
            $(mainSelector).html(template);

            $('#calendar').fullCalendar({
                theme: false,
                header: {
                    left: 'prev,next today addEvent',
                    center: 'title',
                    right: 'month,agendaWeek,agendaDay'
                },
                defaultDate: '2016-01-12',
                selectable: false,
                editable: false,
                eventLimit: true,
                events: data,
                customButtons: {
                    addEvent: {
                        text: 'Add Event',
                        click: function () {
                            Sammy(function () {
                                this.trigger('redirectUrl', {url: '#/calendar/add/'});
                            });
                        }
                    }
                },
                eventClick: function (calEvent, jsEvent, view) {
                    $.get('templates/modal.html', function (templ) {
                        var rendered = Mustache.render(templ, calEvent);
                        $('#modal-body').html(rendered);
                        $('#editLecture').on('click', function() {
                            return false;
                        });
                        $('#deleteLecture').on('click', function() {
                            return false;
                        })
                    });
                    $('#events-modal').modal();
                }
            });

            $('#editLecture').hide();
            $('#deleteLecture').hide();
        });
    }

    function showMyLectures(menuSelector, mainSelector, data) {
        $.get('templates/menu-home.html', function (template) {
            $(menuSelector).html(template);
        });

        $.get('templates/calendar.html', function (template) {
            $(mainSelector).html(template);

            $('#calendar').fullCalendar({
                theme: false,
                header: {
                    left: 'prev,next today addEvent',
                    center: 'title',
                    right: 'month,agendaWeek,agendaDay'
                },
                defaultDate: '2016-01-12',
                selectable: false,
                editable: false,
                eventLimit: true,
                events: data,
                customButtons: {
                    addEvent: {
                        text: 'Add Event',
                        click: function () {
                            Sammy(function () {
                                this.trigger('redirectUrl', {url: '#/calendar/add/'});
                            });
                        }
                    }
                },
                eventClick: function (calEvent, jsEvent, view) {
                    $.get('templates/modal.html', function (templ) {
                        var rendered = Mustache.render(templ, calEvent);
                        $('#modal-body').html(rendered);
                        $('#editLecture').on('click', function() {
                            var lectureId = $('#modal-body').children().first().attr('data-id');

                            Sammy(function () {
                                this.trigger('redirectUrl', {url: '#/calendar/edit/' + lectureId});
                            });
                        });
                        $('#deleteLecture').on('click', function() {
                            var lectureId = $('#modal-body').children().first().attr('data-id');

                            Sammy(function () {
                                this.trigger('redirectUrl', {url: '#/calendar/delete/' + lectureId});
                            });
                        })
                    });
                    $('#events-modal').modal();
                }
            });
        });
    }

    function showAddLecture(menuSelector, mainSelector) {
        $.get('templates/menu-home.html', function (template) {
            $(menuSelector).html(template);
        });

        $.get('templates/add-lecture.html', function (template) {
            $(mainSelector).html(template);

            $('#addLecture').on('click', function () {
                var title = $('#title').val(),
                    start = $('#start').val(),
                    end = $('#end').val();

                Sammy(function () {
                    this.trigger('addLecture', {
                        title: title,
                        start: start,
                        end: end
                    });
                });
            });
        });
    }

    function showEditLecture(menuSelector, mainSelector, data) {
        $.get('templates/menu-home.html', function (template) {
            $(menuSelector).html(template);
        });

        $.get('templates/edit-lecture.html', function (template) {
            var renderedHtml = Mustache.render(template, data);
            $(mainSelector).html(renderedHtml);

            $('#editLecture').on('click', function () {
                var title = $('#title').val(),
                    start = $('#start').val(),
                    end = $('#end').val(),
                    id = data._id;
                // or
                // id = $(this).attr('data-id');

                Sammy(function () {
                    this.trigger('editLecture', {
                        title: title,
                        start: start,
                        end: end,
                        _id: id
                    });
                });
            });
        });
    }

    function showDeleteLecture(menuSelector, mainSelector, data) {
        $.get('templates/menu-home.html', function (template) {
            $(menuSelector).html(template);
        });

        $.get('templates/delete-lecture.html', function (template) {
            var renderedHtml = Mustache.render(template, data);
            $(mainSelector).html(renderedHtml);

            $('#deleteLecture').on('click', function () {
                var id = data._id;
                // or
                // id = $(this).attr('data-id');

                Sammy(function () {
                    this.trigger('deleteLecture', {_id: id});
                });
            });
        });
    }

    return {
        load: function () {
            return {
                showAllLectures: showAllLectures,
                showMyLectures: showMyLectures,
                showAddLecture: showAddLecture,
                showEditLecture: showEditLecture,
                showDeleteLecture: showDeleteLecture
            };
        }
    };
}());