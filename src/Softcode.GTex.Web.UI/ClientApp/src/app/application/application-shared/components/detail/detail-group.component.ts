import { Component, Input } from '@angular/core';

@Component({
    selector: 'app-detail-group',
    templateUrl: './detail-group.component.html',
    styleUrls: ['./detail-group.component.scss']
})
/** detail-group component*/
export class DetailGroupComponent {
    /** detail-group ctor */
    @Input() groupSchemaSource: any=[];
    @Input() entityDataSource: any;
    @Input() componentDataSource: any = [];

    constructor() {

    }
}
