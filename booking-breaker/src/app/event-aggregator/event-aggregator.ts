import { Injectable } from '@angular/core';
import { Subject } from 'rxjs/Subject';
import { Subscription } from 'rxjs/Subscription';
import { IEventBase } from './event.model';

@Injectable()
export class EventAggregator {

    // Tuple cache array
    private events: Array<[string, IEventBase]> = [];

    getEvent<T extends IEventBase>(type: { new (): T; }): T {

        let instance: T;
        const index = this.events.findIndex(item => item[0] === type.toLocaleString());

        if (index > -1) {
            const eventBase: IEventBase = this.events[index][1]; // access second element of the tuple
            return eventBase as T;
        } else {
            instance = new type();
            this.events.push([type.toLocaleString(), instance]);
        }

        return instance;
    }

}
