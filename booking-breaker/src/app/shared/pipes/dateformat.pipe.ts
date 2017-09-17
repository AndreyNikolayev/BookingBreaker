import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'dateformat'
})

export class DateFormatPipe implements PipeTransform {

    private static MONTHS = {
        '01': 'января',
        '02': 'февраля',
        '03': 'марта',
        '04': 'апреля',
        '05': 'мая',
        '06': 'июня',
        '07': 'июля',
        '08': 'августа',
        '09': 'сентября',
        '10': 'октября',
        '11': 'ноября',
        '12': 'декабря',
    };

  transform(value: string, arg?: string): string {
      if (!value) {
            return value;
    }
        try {
            const date = value.split('T');
            const times = date[1].split(':');
            const dates = date[0].split('-');
            return dates[2] + ' ' + DateFormatPipe.MONTHS[dates[1]] + ' ' + times[0] + ':' + times[1];
        }catch (ex) {
           return value;
        }
  }

}
