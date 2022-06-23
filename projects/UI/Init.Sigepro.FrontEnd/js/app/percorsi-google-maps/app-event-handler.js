export class AppEventHandler {
    constructor() {
        this._handlers = new Map();
    }
    aggiungiHandler(evento, callback) {
        if (!this._handlers.has(evento)) {
            this._handlers.set(evento, new Array());
        }
        this._handlers.get(evento).push(callback);
    }
    trigger(evento) {
        if (!this._handlers.has(evento)) {
            return;
        }
        this._handlers.get(evento).forEach((handler) => {
            handler(evento);
        });
    }
    eliminaHandlers() {
        this._handlers.clear();
    }
}
//# sourceMappingURL=app-event-handler.js.map