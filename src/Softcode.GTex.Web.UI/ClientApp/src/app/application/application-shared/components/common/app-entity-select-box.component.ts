import { Component, AfterViewInit, ViewChild, Inject, Input, Output, OnInit, EventEmitter } from '@angular/core';
import { DxSelectBoxModule, DxDropDownBoxComponent, DxSelectBoxComponent, DxDataGridComponent, DxValidatorModule, DxValidationSummaryModule, DxValidationGroupComponent, DxValidatorComponent } from 'devextreme-angular';
import { MessageService } from './../../../../shared/services/message.service'
import { ApiHttpService } from './../../../../shared/services/api-http.service'

import { EntitySelectBoxService } from './../../services/entity-select-box.service';
import DataSource from 'devextreme/data/data_source';
import CustomStore from "devextreme/data/custom_store";
declare var $: any;

@Component({
    selector: 'app-entity-select-box',
    templateUrl: './app-entity-select-box.component.html',
    styleUrls: ['./app-entity-select-box.component.scss'],
})

export class EntitySelectBoxComponent implements OnInit {

    //@Output() onValueChange: EventEmitter<any> = new EventEmitter<any>();
     
    @ViewChild('companyCombobox')
    private companyCombobox: DxDropDownBoxComponent;

    private _isRequired: boolean = false;
    @Input('isRequired') //isRequired = false;
    set isRequired(value: boolean) {
        if (value) {
            this._isRequired = value;
            this.attachValidationToControl();
        }
    }

     
    entityTypeId: any;
    businessProfileId: number;

    @Input('entityType') entityType: string = "";

    @Input('columns') columns: any = [
        {
            "dataField": "name",
            "caption": "Name"
        }];

  

    isDropDownBoxOpened: boolean = false;
    gridDataSource: any = [];
    //_gridBoxValue: number;
    _gridSelectedRowKeys: any[] = [];

    

    @Input('allowMultiSelection') allowMultiSelection = false;
     

    @ViewChild('comboboxValidation')
    private comboboxValidation: DxValidatorComponent;

    /**
     * 
     * @param messageService
     */
    constructor(private messageService: MessageService,
        private entitySelectBoxService: EntitySelectBoxService,
        private apiHttpService: ApiHttpService) {
        

    }

    ngOnInit(): void {
        this.attachValidationToControl();
    }


    attachValidationToControl() {
 
        this.comboboxValidation.validationRules = [{ type: 'custom', validationCallback: this.validationCallback, message: '' }];

        if (this._isRequired) {
            this.comboboxValidation.validationRules = [{ type: 'required', message: this.entityType + ' is required.' }];
        }
    }

    validationCallback() {

        return true;
    }

    setColumns(columns: any) {
        this.columns = columns;
    }

    setDataSource(serviceUrl: string) {
        if (serviceUrl != null && serviceUrl != undefined) {
            this.gridDataSource = this.makeAsyncDataSource(serviceUrl, this.entitySelectBoxService);
        }
    }

    private makeAsyncDataSource(serviceUrl: string, entitySelectBoxService: EntitySelectBoxService) {
        return new CustomStore({
            loadMode: "raw",
            key: "id",
            load: () => {
                return entitySelectBoxService.getDataSource(serviceUrl).toPromise().then((response: any) => {
                    return response.result;
                })
            }
        });
    }

    setEntityDataSource(entityTypeId: number, businessProfileId: number): void {

        if (entityTypeId > 0 && businessProfileId > 0
            && !(this.entityTypeId == entityTypeId && this.businessProfileId == businessProfileId)) {
            this.entityTypeId = entityTypeId;
            this.businessProfileId = businessProfileId
             
            this.gridDataSource = this.makeAsyncEntityDataSource(entityTypeId, businessProfileId, this.entitySelectBoxService);
        }
    }
    private makeAsyncEntityDataSource(entityTypeId: number, businessProfileId: number, entitySelectBoxService: EntitySelectBoxService) {
        return new CustomStore({
            loadMode: "raw",
            key: "id",
            load: () => {
                return entitySelectBoxService.getEntitiesByBusinessProfileId(entityTypeId, businessProfileId).toPromise().then((response: any) => {
                    return response.result;
                })
            }
        });
    };

    setCompanyDataSource(businessProfileId: number): void {
        if (this.businessProfileId != businessProfileId) {

            this.businessProfileId == businessProfileId;
            this.gridDataSource = this.makeAsyncCompaniesDataSource(businessProfileId, this.entitySelectBoxService);
        }
    }
    private makeAsyncCompaniesDataSource(businessProfileId: number, entitySelectBoxService: EntitySelectBoxService) {
        return new CustomStore({
            loadMode: "raw",
            key: "id",
            load: () => {
                return entitySelectBoxService.getCompaniesByBusinessProfileId(businessProfileId).toPromise().then((response: any) => {
                    return response.result;
                })
            }
        });
    };

    setContactDataSource(businessProfileId: number): void {
        if (this.businessProfileId != businessProfileId) {

            this.businessProfileId == businessProfileId;
            this.gridDataSource = this.makeAsyncContactsDataSource(businessProfileId, this.entitySelectBoxService);
        }
    }
    private makeAsyncContactsDataSource(businessProfileId: number, entitySelectBoxService: EntitySelectBoxService) {
        return new CustomStore({
            loadMode: "raw",
            key: "id",
            load: () => {
                return entitySelectBoxService.getContactsByBusinessProfileId(businessProfileId).toPromise().then((response: any) => {
                    return response.result;
                })
            }
        });
    };

    onGridInitialized(e): void {        
        if (this.allowMultiSelection) {
            setTimeout(function () {
                e.component.option('selection', { mode: 'multiple' });
            }, 200);
        }
    }

    //get gridBoxValue(): number {
    //    return this._gridBoxValue;
    //}
     
    //set gridBoxValue(value: number) {
    //    if (value == null) {
    //        this._gridSelectedRowKeys = [];
    //    }
    //    //this._gridSelectedRowKeys = value && [value] || [];
    //    this._gridBoxValue = value;
    //}



    get gridBoxValue(): any[] {
        return this._gridSelectedRowKeys;
    }

    set gridBoxValue(value: any[]) {
        if (value == null) {
            this._gridSelectedRowKeys = [];
        }
        this._gridSelectedRowKeys = value || [] ;
        //this._gridBoxValue = value;
    }


    get gridSelectedRowKeys(): any[] {
        return this._gridSelectedRowKeys;
    }

    set gridSelectedRowKeys(value: any[]) {
        //this._gridBoxValue = value.length && value[0] || null;
        this._gridSelectedRowKeys = value;
    }
    
    //gridBox_displayExpr(item) {
    //    //console.log(item && item.name);
    //    return item && item.name;
        
    //}

    setValue(id: any): void {
        if (this.allowMultiSelection) throw new Error('Invalid for single selection mode');
        this._gridSelectedRowKeys.push(id);
    }

    getValue(): any {
        if (this.allowMultiSelection) throw new Error('Invalid for single selection mode');

        return this._gridSelectedRowKeys.length > 0 ? this._gridSelectedRowKeys[0] : 0;
    }

    @Input('selectedId')
    set selectedId(id: any) {
        
        if (this.allowMultiSelection) throw new Error('Invalid for single selection mode');
        this._gridSelectedRowKeys.push(id);
    }
  
    get selectedId(): any {
        if (this.allowMultiSelection) throw new Error('Invalid for single selection mode');

        return this.gridSelectedRowKeys.length > 0 ? this._gridSelectedRowKeys[0] : null;
    }

    set selectedIds(value: any[]) {
        if (!this.allowMultiSelection) throw new Error('Invalid for multiple selection mode');

        if (value == null) {
            this._gridSelectedRowKeys = [];
        }

        this._gridSelectedRowKeys = value;
    }
    get selectedIds(): any[] {
        if (!this.allowMultiSelection) throw new Error('Invalid for multiple selection mode');
        return this._gridSelectedRowKeys;
    }


    onSelectionChanged(e) {
        if (!this.allowMultiSelection) {
            this.isDropDownBoxOpened = false;
        }
    }

    onValueChanged(e) {

        let newValue: any;

        if (this.allowMultiSelection) {
            newValue = this.selectedIds;
        }
        else {
            newValue = this.selectedId;
        }

        //this.onValueChange.emit(newValue);
    }

}
