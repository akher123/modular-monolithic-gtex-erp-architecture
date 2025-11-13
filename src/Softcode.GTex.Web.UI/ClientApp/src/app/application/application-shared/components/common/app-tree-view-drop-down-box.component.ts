import { Component, AfterViewInit, ViewChild, Inject, Input, Output, OnInit, AfterContentChecked, EventEmitter } from '@angular/core';
import { DxSelectBoxModule, DxDropDownBoxComponent, DxSelectBoxComponent,  DxValidatorModule, DxValidationSummaryModule, DxValidationGroupComponent, DxValidatorComponent, DxTreeViewComponent } from 'devextreme-angular';
import { MessageService } from './../../../../shared/services/message.service'
import { ApiHttpService } from './../../../../shared/services/api-http.service'
 
import DataSource from 'devextreme/data/data_source';
import CustomStore from "devextreme/data/custom_store";
import { UtilityService } from './../../services/utility.service';
declare var $: any;

@Component({
    selector: 'app-tree-view-drop-down-box',
    templateUrl: './app-tree-view-drop-down-box.component.html',
    styleUrls: ['./app-tree-view-drop-down-box.component.scss'],
})

export class TreeViewDropDownBoxComponent implements OnInit {

    @Input('disabled') disabled: boolean = false; 
    private _isRequired: boolean = false;
    @Input('isRequired') //isRequired = false;
    set isRequired(value: boolean) {
        if (value) {
            this._isRequired = value;
            this.attachValidationToControl();
        }
    }
    @Input('labelFor') labelFor: string = "";
    isDropDownBoxOpened: boolean = false;
    treeDataSource: any;
    treeBoxValue: string = "";
    @Input('selectedValue')
    set seletedValue(value: any) {
        this.treeBoxValue = value;
    } 
    get seletedValue() {
        return this.treeBoxValue;
    }


    @Output() onSelectedValueChange: EventEmitter<any> = new EventEmitter<any>();

    @Output() onClearButtonClick: EventEmitter<any> = new EventEmitter<any>();

    @ViewChild(DxTreeViewComponent) treeView;

    
    @ViewChild('treeViewDropDownValidation')
    private treeViewDropDownValidation: DxValidatorComponent;

    /**
     * 
     * @param messageService
     */
    constructor(private messageService: MessageService,
        private utilityService: UtilityService,
        private apiHttpService: ApiHttpService) {
    }

    ngOnInit(): void {
        this.attachValidationToControl();
    }
    attachValidationToControl() {

        this.treeViewDropDownValidation.validationRules = [{ type: 'custom', validationCallback: this.validationCallback, message: '' }];

        if (this._isRequired) {
            this.treeViewDropDownValidation.validationRules = [{ type: 'required', message: this.labelFor + ' is required.' }];
        }
    }

    validationCallback() {

        return true;
    }

    syncTreeViewSelection(e:any) {
        if (!this.treeView) return;

        if (!this.treeBoxValue) {
            this.onClearButtonClick.emit();
            this.treeView.instance.unselectAll();
        } else {
            this.treeView.instance.selectItem(this.treeBoxValue);
        }
    }

    getSelectedItemsKeys(items) {
        var result = [],
            that = this;

        items.forEach(function (item) {
            if (item.selected) {
                result.push(item.key);
            }
            if (item.items.length) {
                result = result.concat(that.getSelectedItemsKeys(item.items));
            }
        });
        return result;
    }

    treeView_itemSelectionChanged(e) {
        this.isDropDownBoxOpened = false;

        const nodes = e.component.getNodes();
        this.treeBoxValue = this.getSelectedItemsKeys(nodes).join("");
        this.onSelectedValueChange.emit(this.treeBoxValue);
    }


    private makeAsyncDataSource(utilityService: UtilityService, url: string) {
        return new CustomStore({
            loadMode: "raw",
            key: "sId",
            load: () => {
                return utilityService.getDataSource(url).toPromise().then((response: any) => {
                    return response.result;
                })
            }
        });
    }
    setDatasource(url: string): void {
        this.clearDatasource();
        this.treeDataSource = this.makeAsyncDataSource(this.utilityService, url);
    }


    clearDatasource(): void {
        this.seletedValue = null;
        this.treeDataSource = [];
    }

}
