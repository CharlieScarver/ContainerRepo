var app = app || {};

app.notesViewBag = (function (){
    //TODO: Events

    function showOfficeNotes(selector, data) {
        $.get('templates/officeNoteTemplate.html', function (template) {
            var renderedHtml = Mustache.render(template, data);
            $(selector).html(renderedHtml);
        });
    }

    function showMyNotes(selector, data) {
        $.get('templates/myNoteTemplate.html', function (template) {
            var renderedHtml = Mustache.render(template, data);
            $(selector).html(renderedHtml);

            $('.edit').on('click', function () {
                var noteId = $(this).parent().attr('data-id'),
                    note = data.notes.filter(function (a) {
                        return a.id == noteId;
                    });

                if (note.length) {
                    Sammy(function () {
                        this.trigger('showEditNote', note[0]);
                    });
                }
            });

            $('.delete').on('click', function () {
                var noteId = $(this).parent().attr('data-id'),
                    note = data.notes.filter(function (a) {
                        return a.id == noteId;
                    });

                if (note.length) {
                    Sammy(function () {
                        this.trigger('showDeleteNote', note[0]);
                    });
                }
            });
        });
    }

    function showAddNote(selector) {
        $.get('templates/addNote.html', function (template) {
            $(selector).html(template);

            $('#addNoteButton').on('click', function () {
                var title = $('#title').val(),
                    text = $('#text').val(),
                    deadline = $('#deadline').val();

                Sammy(function () {
                    this.trigger('addNote', {
                        title: title,
                        text: text,
                        deadline: deadline
                    });
                });
            });
        });
    }

    function showEditNote(selector, data) {
        $.get('templates/editNote.html', function (template) {
            var renderedHtml = Mustache.render(template, data);
            $(selector).html(renderedHtml);

            $('#editNoteButton').on('click', function () {
                var title = $('#title').val(),
                    text = $('#text').val(),
                    deadline = $('#deadline').val(),
                    noteId = $(this).parent().parent().attr('data-id');

                Sammy(function () {
                    this.trigger('editNote', {
                        title: title,
                        text: text,
                        deadline: deadline,
                        id: noteId
                    });
                });
            });
        });
    }

    function showDeleteNote(selector, data) {
        $.get('templates/deleteNote.html', function (template) {
            var renderedHtml = Mustache.render(template, data);
            $(selector).html(renderedHtml);

            $('#deleteNoteButton').on('click', function () {
                var noteId = $(this).parent().parent().attr('data-id');

                Sammy(function () {
                    this.trigger('deleteNote', {
                        id: noteId
                    });
                });
            });
        });
    }

    return {
        load: function () {
            return {
                showOfficeNotes: showOfficeNotes,
                showMyNotes: showMyNotes,
                showAddNote: showAddNote,
                showEditNote: showEditNote,
                showDeleteNote: showDeleteNote
            };
        }
    };
}());