import { Directive, ElementRef, Input, OnInit, Renderer2 } from '@angular/core';
import { ShowtimePlace } from './../../../../API/models/showtime-place';

@Directive({
  selector: '[appShowtimePlaceStyle]'
})
export class ShowtimePlaceStyleDirective implements OnInit {

  constructor(private el: ElementRef, private renderer: Renderer2) { }

  @Input() showplace: ShowtimePlace;

  private highlight(color: string) {
    this.el.nativeElement.style.backgroundColor = color;
  }

  ngOnInit() {
    this.el.nativeElement.style.top = this.showplace.ShowTimePlaceStyle.Top + 'px';
    this.el.nativeElement.style.left = this.showplace.ShowTimePlaceStyle.Left + 'px';
    this.el.nativeElement.style.width = this.showplace.ShowTimePlaceStyle.Width + 'px';
    this.el.nativeElement.style.height = this.showplace.ShowTimePlaceStyle.Height + 'px';

    if (this.showplace.ShowTimePlaceStyle.Height > 50) {
        this.renderer.addClass(this.el.nativeElement, 'vip-place');
    }

    switch (this.showplace.PlaceAccess) {
        case 0:
            {
                this.renderer.addClass(this.el.nativeElement, 'open-seat');
                break;
            }
        case 1:
            {
                this.renderer.addClass(this.el.nativeElement, 'taken-seat');
                if (this.showplace.ShowTimePlaceStyle.Height > 50) {
                    this.renderer.addClass(this.el.nativeElement, 'taken-vip');
                }
                break;
            }
        case 2:
            {
                this.renderer.addClass(this.el.nativeElement, 'broned-seat');
                break;
            }
        default:
            {
                this.renderer.addClass(this.el.nativeElement, 'disabled-seat');
                break;
            }
    }
  }
}
