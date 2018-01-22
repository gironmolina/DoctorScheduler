import { Directive, Input, OnInit, ElementRef, Renderer2 } from '@angular/core';

import * as octicons from 'octicons';

@Directive({
    selector: '[octicon]'
})
export class OcticonDirective implements OnInit {

    @Input() octicon: string;
    @Input() fill: string;
    @Input() width: number;
    @Input() height: number;

    constructor(private elementRef: ElementRef, private renderer: Renderer2) { }

    ngOnInit(): void {
        const el: HTMLElement = this.elementRef.nativeElement;
        el.innerHTML = octicons[this.octicon].toSVG();

        const icon: Node = el.firstChild;
        if (this.fill) {
            this.renderer.setStyle(icon, 'fill', this.fill)
        }
        if (this.width) {
            this.renderer.setStyle(icon, 'width', this.width);
        }
        if (this.height) {
            this.renderer.setStyle(icon, 'height', this.height);
        }
    }

}