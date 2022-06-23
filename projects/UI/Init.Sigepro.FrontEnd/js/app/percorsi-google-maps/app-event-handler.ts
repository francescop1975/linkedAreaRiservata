export class AppEventHandler<T> {

    private _handlers = new Map<T, Array<(evento: T) => void>>();

    aggiungiHandler(evento: T, callback: (evento: T) => void): void {
        if (!this._handlers.has(evento)) {
            this._handlers.set(evento, new Array<() => void>());
        }

        this._handlers.get(evento).push(callback);
    }

    trigger(evento: T): void {
        if (!this._handlers.has(evento)) {
            return;
        }

        this._handlers.get(evento).forEach((handler) => {
            handler(evento);
        });
    }

    eliminaHandlers(): void {
        this._handlers.clear();
    }
    
}