import { Injectable } from '@angular/core';
import { Subject } from 'rxjs/Subject';
import { Subscription } from 'rxjs/Subscription';
import { IEventBase } from './event.model';


@Injectable()
export class PubEvent<T> extends IEventBase {

    // Observable string source (RxJS)
    private source = new Subject<T>();

    // Observable string streams (RxJS)
    private observable = this.source.asObservable();

    // Cache array of tuples
    private subscriptions: Array<[(a: T) => void, Subscription]> = [];

    subscribe(observer: (a: T) => void) {
        if (this.subscriptions.find(item => item[0] === observer) !== undefined) {
            return;
        }

        const subscription = this.observable.subscribe(observer);
        this.subscriptions.push([observer, subscription]);
        console.log('subscribed Message');
    }

    publish(data: T) {
        this.source.next(data);
        console.log('pushed Message');
    }

    unsubscribe(observer: (a: T) => void) {
        const foundIndex = this.subscriptions.findIndex(item => item[0] === observer);
        if (foundIndex > -1) {
            const subscription: Subscription = this.subscriptions[foundIndex][1];
            if (subscription.closed === false) {
                subscription.unsubscribe();
                console.log('unsubscribe successful');
            }

            this.subscriptions.splice(foundIndex, 1); // removes item
        }
    }

}
