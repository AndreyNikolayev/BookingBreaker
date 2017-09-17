import { NgModule } from '@angular/core';
import {DateFormatPipe} from './pipes/dateformat.pipe';
import {ShowtimePlaceStyleDirective} from './directives/style-directives/showplace-style-directive/showplace-style.directive';

@NgModule({
    imports: [],
    declarations: [DateFormatPipe, ShowtimePlaceStyleDirective],
    exports: [DateFormatPipe, ShowtimePlaceStyleDirective]
})
export class SharedModule { }
