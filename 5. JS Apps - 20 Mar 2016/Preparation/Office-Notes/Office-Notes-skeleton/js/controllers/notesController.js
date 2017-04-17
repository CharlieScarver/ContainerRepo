var app = app || {};

app.notesController = (function (){
    function NotesController(viewBag, model) {
        this._viewBag = viewBag;
        this._model = model;
    }

    //kinvey pagination => &limit=20&skip=40 (page 3)
    NotesController.prototype.loadOfficeNotes = function(selector){
        var date = new Date().toISOString().substr(0,10),
            _this = this,
            data = {};
        return this._model.getNotesForToday(date)
            .then(function (success) {
                data.notes = success;
                _this._viewBag.showOfficeNotes(selector, data);
            }).done();
    };

    NotesController.prototype.loadMyNotes = function(selector) {
        var userId = sessionStorage['userId'],
            _this = this;

        return this._model.getNotesByCreatorId(userId)
            .then(function (success) {
                var result = {
                    notes: []
                };

                success.forEach(function (note) {
                    result.notes.push({
                        title: note.title,
                        text: note.text,
                        author: note.author,
                        deadline: note.deadline,
                        id: note._id
                    });
                });

                _this._viewBag.showMyNotes(selector, result);
            }).done();
    };

    NotesController.prototype.loadAddNote = function(selector) {
        this._viewBag.showAddNote(selector);
    };

    NotesController.prototype.addNote = function(data) {
        var result = {
            title: data.title,
            text: data.text,
            deadline: data.deadline,
            author: sessionStorage['username']
        };

        return this._model.addNote(result)
            .then(function (success) {
                console.log(success);

                Sammy(function () {
                    noty({
                        theme: 'relax',
                        text: 'Note was successfully added!',
                        type:'success',
                        timeout: 2000,
                        closeWith: ['click']
                    });
                    this.trigger('redirectUrl', {url: '#/myNotes/'});
                });
            }, function (error) {
                noty({
                    theme: 'relax',
                    text: error.responseJSON.error || 'A problem occurred while adding note!',
                    type:'error',
                    timeout: 4000,
                    closeWith: ['click']
                });
            });
    };

    NotesController.prototype.loadEditNote = function(selector, data) {
        this._viewBag.showEditNote(selector, data);
    };

    NotesController.prototype.editNote = function(noteId, data) {
        var result = {
            title: data.title,
            text: data.text,
            deadline: data.deadline,
            author: sessionStorage['username']
        };

        return this._model.editNote(noteId, result)
            .then(function (success) {
                Sammy(function () {
                    noty({
                        theme: 'relax',
                        text: 'Note was successfully edited!',
                        type:'success',
                        timeout: 2000,
                        closeWith: ['click']
                    });
                    this.trigger('redirectUrl', {url: '#/myNotes/'});
                });
            }, function (error) {
                noty({
                    theme: 'relax',
                    text: error.responseJSON.error || 'A problem occurred while editing note!',
                    type:'error',
                    timeout: 4000,
                    closeWith: ['click']
                });
            });
    };

    NotesController.prototype.loadDeleteNote = function(selector, data) {
        this._viewBag.showDeleteNote(selector, data);
    };

    NotesController.prototype.deleteNote = function(noteId) {
        return this._model.deleteNote(noteId)
            .then(function (success) {
                Sammy(function () {
                    this.trigger('redirectUrl', {url: '#/myNotes/'});
                    noty({
                        theme: 'relax',
                        text: 'Note was successfully deleted!',
                        type:'success',
                        timeout: 2000,
                        closeWith: ['click']
                    });
                });
            }, function (error) {
                noty({
                    theme: 'relax',
                    text: error.responseJSON.error || 'A problem occurred while deleting note!',
                    type:'error',
                    timeout: 4000,
                    closeWith: ['click']
                });
            });
    };

    return {
        load: function (viewBag, model) {
            return new NotesController(viewBag, model);
        }
    };
}());