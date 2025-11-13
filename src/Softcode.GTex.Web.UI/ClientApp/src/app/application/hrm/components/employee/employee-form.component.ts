import { Component, AfterViewInit, ViewChild, Inject, Input, AfterContentChecked } from '@angular/core';
import { DxSelectBoxModule, DxTextAreaModule, DxDateBoxModule, DxFormModule, DxNumberBoxModule, DxTextBoxModule, DxCheckBoxModule, DxValidatorModule, DxValidationGroupComponent, DxColorBoxComponent, DxTreeListComponent, DxTreeListModule, DxFileUploaderComponent, DxValidatorComponent, DxSelectBoxComponent, DxTextAreaComponent, DxTagBoxComponent, DxRadioGroupComponent, DxDropDownBoxComponent, DxDataGridComponent } from 'devextreme-angular';
import { MessageService } from './../../../../shared/services/message.service'
import { TitlebarComponent } from './../../../application-shared/components/titlebar/titlebar.component';
import { ImageUploadComponent } from './../../../application-shared/components/common/image-upload.component';
import { ActivatedRoute, Router, Params } from '@angular/router';
import { ToolbarType, DetailPageAction, PatterMatch } from './../../../application-shared/components/titlebar/utilities';
import { retry } from 'rxjs/operators';

import DataSource from 'devextreme/data/data_source';
import CustomStore from "devextreme/data/custom_store";
import { HttpClient, HttpClientModule, HttpParams } from "@angular/common/http";
import { ToolbarItem, ToolbarItemOption } from '../../../application-shared/components/titlebar/toolbar-item';
import { EntitySelectBoxComponent } from '../../../application-shared/components/common/app-entity-select-box.component';
import { EmployeeService } from '../../services/employee.service';
import { CompanyService } from '../../../crm/services/company.service';
import { NavigationService } from './../../../../shared/services/navigation.service';
import { InlineAddressComponent } from './../../../system-service/components/address/inline-address.component'
import "rxjs/add/operator/toPromise";
declare var $: any;

@Component({
    selector: 'app-employee-form',
    templateUrl: './employee-form.component.html',
    styleUrls: ['./employee-form.component.scss']
})

export class EmployeeFormComponent implements AfterContentChecked {

    //@Input() entityModel: EntityModel = new EntityModel();

    @Input('EmployeeId') employeeId: number = 0;

    toolbarType: ToolbarType = ToolbarType.DetailPage;
    minDate: Date = new Date(1900, 0, 1);
    nowDate: Date = new Date();
    maxDate: Date = new Date(this.nowDate.getFullYear() - 18, this.nowDate.getMonth(), this.nowDate.getDate());
    emailPattern: any = PatterMatch.EmailPattern;
    contentClass: any = "detail-page-content-div";

    employeeDataSource: any = [];
    emptyEmployeeDataSource: any = [];
    contactDataSource: any = [];
    photoDataSource: any = [];

    businessProfileSelectItems: any = [];

    titleSelectItems: any = [];
    positionSelectItems: any = [];
    timeZoneSelectItems: any = [];
    skillsSelectItems: any = [];
    imTypeSelectItems: any = [];
    preferredContactSelectItems: any = [];
    genderSelectItems: any = [];

    regionSelectItems: any = [];
    //costCentreSelectItems: any = [];
    departmentSelectItems: any = [];
    businessUnitSelectItems: any = [];
    employmentTypeSelectItems: any[];



    isDefaultBusinessProfileUser: boolean = false;
    disabledBusinessProfileUser = true;

    companySelectedValue: any;

    contactSpecialisations: any = [];
    companyIds: any = [];
    //businessProfileIds: any = [];
    businessProfileId: any;

    preferredPhoneTypeSelectItem1: any = [];
    preferredPhoneTypeSelectItem2: any = [];
    preferredPhoneTypeSelectItem3: any = [];

    showEmail2Field: boolean = false;
    showEmail3Field: boolean = false;

    showIM2Field: boolean = false;
    showIM3Field: boolean = false;

    toolbarAdditionalItems: ToolbarItem[];

    @ViewChild(TitlebarComponent)
    private titlebar: TitlebarComponent;

    @ViewChild('imgUploadControl')
    private imageUploadControl: ImageUploadComponent;

    @ViewChild('formValidation')
    private formValidation: DxValidationGroupComponent;

    @ViewChild('addressControl')
    private addressControl: InlineAddressComponent;


    @ViewChild('supervisorEntitySelectionBox')
    private supervisorEntitySelectionBox: EntitySelectBoxComponent;

    @ViewChild('costCentreEntitySelectionBox')
    private costCentreEntitySelectionBox: EntitySelectBoxComponent;

    @ViewChild('siteEntitySelectionBox')
    private siteEntitySelectionBox: EntitySelectBoxComponent;



    @ViewChild('businessProfileValidation')
    private businessProfileValidation: DxValidatorComponent;

    @ViewChild('firstNameValidation')
    private firstNameValidation: DxValidatorComponent;

    @ViewChild('lastNameValidation')
    private lastNameValidation: DxValidatorComponent;

    @ViewChild('emailValidation')
    private emailValidation: DxValidatorComponent;

    @ViewChild('email2Validation')
    private email2Validation: DxValidatorComponent;

    @ViewChild('email3Validation')
    private email3Validation: DxValidatorComponent;

    @ViewChild('employeeIdValidation')
    private employeeIdValidation: DxValidatorComponent;


    /*********************************************** Event Start ****************************************/

    /**
     * Constructor
     * @param roleService
     * @param userService
     * @param messageService
     * @param route
     * @param router
     */
    constructor(private employeeService: EmployeeService,
        private companyService: CompanyService,
        private messageService: MessageService,
        private navigationService: NavigationService,
        private route: ActivatedRoute,
        private router: Router) {


        this.route.params.subscribe(params => {
            if (params['employeeId'] !== undefined) {
                this.employeeId = params['employeeId'];
                this.toolbarType = ToolbarType.DetailTabPage;
                this.contentClass = "detail-page-content-div-tab";
            }
        });
    }

    /**
     * Event
     **/
    ngAfterContentChecked(): void {
        this.disabledBusinessProfileUser = this.employeeId > 0 || !this.isDefaultBusinessProfileUser;

        if (this.employeeId == 0) {

            var offset = new Date().getTimezoneOffset() * -1;
            var defaultTZ = this.timeZoneSelectItems.filter(x => x.tag == offset);
            if (defaultTZ.length > 0) {
                this.contactDataSource.timeZoneId = defaultTZ[0].id;
            }

            let defaultRegion = this.regionSelectItems.filter(x => x.isDefault)
            if (defaultRegion.length > 0) {
                this.employeeDataSource.regionId = defaultRegion[0]['id'];
            }
        } 
    }
    /**
     * Event
     **/
    ngOnInit(): void {

        $(".dx-fileuploader").find(".dx-button-content").css("background-color", "transparent!important");
        this.supervisorEntitySelectionBox.setColumns(this.employeeService.getSupervisorColumns());
        this.siteEntitySelectionBox.setColumns(this.employeeService.getBPSiteColumns())
        this.costCentreEntitySelectionBox.setColumns(this.employeeService.getCostCentreColumns());
        this.getEmployeeDetails();
        this.attachValidationToControl();

    }

    /**
    * Email & IM Type Start
    * */

    showEmail2(): void {
        this.showEmail2Field = true;
    }
    showEmail3(): void {
        this.showEmail3Field = true;
    }
    removeEmail2(): void {
        this.showEmail2Field = false;
    }
    removeEmail3(): void {
        this.showEmail3Field = false;
    }

    showIM2(): void {
        this.showIM2Field = true;
    }
    showIM3(): void {
        this.showIM3Field = true;
    }
    removeIM2(): void {
        this.showIM2Field = false;
    }
    removeIM3(): void {
        this.showIM3Field = false;
    }

    imTypeSelectionChange(e): void {
        if (this.contactDataSource.imTypeId < 1)
            this.contactDataSource.imLoginId = '';
    }
    imType2SelectionChange(e): void {
        if (this.contactDataSource.imTypeId2 < 1)
            this.contactDataSource.imLoginId2 = '';
    }
    imType3SelectionChange(e): void {
        if (this.contactDataSource.imTypeId3 < 1)
            this.contactDataSource.imLoginId3 = '';
    }

    /**
    * Email & IM Type end
    * */


    /**
    * on save button clicked
    */
    onSaveClicked(e): void {

        this.validateAndSave(DetailPageAction.Save);
    }

    /**
     * on save and new button clicked
     */
    onSaveNNewClicked(e): void {


        this.validateAndSave(DetailPageAction.SaveAndNew);
    }

    /**
     * on save and close button clicked
     */
    onSaveNCloseClicked(e): void {


        this.validateAndSave(DetailPageAction.SaveAndClose);
    }

    /**
    * on close button clicked
    */
    onCloseClicked(e): void {


        this.redirectToListPage(DetailPageAction.Close);
    }

    /************************************************ Method Start *********************************** */

    /**
     * On value changed
     **/
    onValueChanged(e): void {

        //let newValue = e.value;
        //if (newValue > 0)
        //    this.populateCompanyOnBusinessProfileSelectionChanged(newValue);

    }

     
    /**
     * attach validation to the controls
     *
     * */
    attachValidationToControl() {

        //validation        
        this.businessProfileValidation.validationRules = [{ type: 'required', message: 'Business Profile is required.' }];
        //this.companyValidation.validationRules = [{ type: 'required', message: 'Company is required.' }];
        this.firstNameValidation.validationRules = [{ type: 'required', message: 'First Name is required.' }];
        this.lastNameValidation.validationRules = [{ type: 'required', message: 'Last Name is required.' }];
        this.employeeIdValidation.validationRules = [{ type: 'required', message: 'Employee ID is required.' }];


        this.emailValidation.validationRules = [{ type: 'required', message: 'Email is required.' },
        { type: 'pattern', pattern: this.emailPattern, message: 'Email is invalid' }];
        //this.email2Validation.validationRules = [{ type: 'pattern', pattern: this.emailPattern, message: 'Email 2 is invalid' }];
        //this.email3Validation.validationRules = [{ type: 'pattern', pattern: this.emailPattern, message: 'Email 3 is invalid' }];

    }

    onBusinessProfileValueChanged(e): void {
        this.addressControl.LoadAddressTypeSelectBoxByBpId(e.value);
        this.getSelectBoxByBPIdData();
        this.supervisorEntitySelectionBox.setDataSource(this.employeeService.getSupervisorServiceUrl(this.businessProfileId, this.employeeId));
        this.siteEntitySelectionBox.setDataSource(this.employeeService.getBPSiteServiceUrl(this.businessProfileId));
        this.costCentreEntitySelectionBox.setDataSource(this.employeeService.getCostCentreServiceUrl(this.businessProfileId));
    }

    /**
     * Init method
     **/
    getEmployeeDetails(): void {

        this.employeeService.getEmployeeDetails(this.employeeId).toPromise().then((response: any) => {

            if (response == undefined || response == null) return;

            
            this.isDefaultBusinessProfileUser = response.result.isDefaultBusinessProfile;

            this.emptyEmployeeDataSource = response.result.emptyEmployee;
            this.setEmployeeDatasource(response.result.employee);

            this.businessProfileSelectItems = response.result.businessProfileSelectItems;
            this.timeZoneSelectItems = response.result.timezoneSelectItems;

            this.preferredPhoneTypeSelectItem1 = [];
            this.preferredPhoneTypeSelectItem2 = [];
            this.preferredPhoneTypeSelectItem3 = [];
            this.preferredPhoneTypeSelectItem1.push(response.result.preferredPhoneTypeSelectItems[0]);
            this.preferredPhoneTypeSelectItem2.push(response.result.preferredPhoneTypeSelectItems[1]);
            this.preferredPhoneTypeSelectItem3.push(response.result.preferredPhoneTypeSelectItems[2]);

            this.titlebar.initializeToolbar(this.employeeId == 0 ? "Employee: New" : "Employee: " + this.contactDataSource.firstName + " " + this.contactDataSource.lastName, null, this.toolbarType);
        });

    }

    setEmployeeDatasource(employeeModel: any): void {

        this.employeeDataSource = employeeModel;
        this.employeeId = employeeModel.id;
        this.contactDataSource = this.employeeDataSource.contact;
        this.contactSpecialisations = this.contactDataSource.contactSpecialisationIds;
        this.businessProfileId = this.contactDataSource.businessProfileIds[0];

        this.photoDataSource = this.contactDataSource.photo;
        this.imageUploadControl.setEntityValue(this.photoDataSource);

        if (this.employeeId > 0) {
            this.supervisorEntitySelectionBox.selectedId = this.employeeDataSource.supervisorId

            this.siteEntitySelectionBox.selectedIds = this.employeeDataSource.employeeSiteIds;
            this.costCentreEntitySelectionBox.selectedIds = this.employeeDataSource.employeeCostCentreIds;
        }

        this.showEmail2Field = this.contactDataSource.email2 != null && this.contactDataSource.email2 != '';
        this.showEmail3Field = this.contactDataSource.email3 != null && this.contactDataSource.email3 != '';

        this.showIM2Field = this.contactDataSource.imLoginId2 != null && this.contactDataSource.imLoginId2 != '';
        this.showIM3Field = this.contactDataSource.imLoginId3 != null && this.contactDataSource.imLoginId3 != '';
    }

    getSelectBoxByBPIdData(): void {

        this.employeeService.getEmployeeSelectBoxData(this.businessProfileId).toPromise().then((response: any) => {

            this.imTypeSelectItems = response.result.imTypeSelectItems;


            this.titleSelectItems = response.result.titleSelectItems;
            this.positionSelectItems = response.result.positionSelectItems;

            this.skillsSelectItems = response.result.skillsSelectItems;
            this.genderSelectItems = response.result.genderSelectItems;


            this.preferredContactSelectItems = response.result.preferredContactMethodSelectItems;
            //this.preferredPhoneTypeSelectItems = response.result.preferredPhoneTypeSelectItems;
            this.businessUnitSelectItems = response.result.businessUnitSelectItems;
            this.departmentSelectItems = response.result.departmentSelectItems;

            this.regionSelectItems = response.result.regionSelectItems;

            //this.costCentreSelectItems = response.result.costCentreSelectItems;
            this.employmentTypeSelectItems = response.result.employmentTypeSelectItems;

        });

    }

    /**
     * 
     * @param closedWindow
     * @param isNew
     */
    saveEntity(action: DetailPageAction): void {

        this.contactDataSource.photo = this.imageUploadControl.photoDataSource;
        this.contactDataSource.contactSpecialisationIds = this.contactSpecialisations;
        this.contactDataSource.businessProfileIds = [];
        this.contactDataSource.businessProfileIds.push(this.businessProfileId);
        this.employeeDataSource.contact = this.contactDataSource;
        this.employeeDataSource.employeeCostCentreIds = this.costCentreEntitySelectionBox.selectedIds;
        this.employeeDataSource.employeeSiteIds = this.siteEntitySelectionBox.selectedIds;

        //Custom component 
        this.employeeDataSource.supervisorId = this.supervisorEntitySelectionBox.selectedId;

        this.employeeDataSource.employeeSiteIds = this.siteEntitySelectionBox.selectedIds;
        this.employeeDataSource.employeeCostCentreIds = this.costCentreEntitySelectionBox.selectedIds;


        if (this.employeeId == 0) {

            this.employeeDataSource.contact.contactAddresses = this.addressControl.getAddressesFromMemory();

            this.employeeService.createEmployee(this.imageUploadControl.fileName, this.employeeDataSource).subscribe(data => {
                this.messageService.success("Record has been saved successfully", 'Information');
                this.employeeId = data.result;
                this.employeeDataSource.id = data.result;

                this.redirectToListPage(action);
            });
        }
        else {
            this.employeeService.updateEmployee(this.imageUploadControl.fileName, this.employeeId, this.employeeDataSource).subscribe(data => {
                this.messageService.success("Record has been updated successfully", 'Information');

                this.imageUploadControl.photoDataSource.isUpdated = false;
                this.redirectToListPage(action);
            });
        }
    }

    /**
     * validate and save data
     */
    validateAndSave(action: DetailPageAction): void {

        if (!this.formValidation.instance.validate().isValid) {
            return;
        }

        if (!this.addressControl.validateAndSave()) {
            return;
        }

        this.saveEntity(action);
    }

    /**
     * st company value to combobox
     * @param companyId
     */
    setCompanyValue(companyIds: any[]) {
        this.companyIds = companyIds;
    }
     
    /**
    * redirect to list page
    */
    redirectToListPage(action: DetailPageAction): void {

        var newNavigationUrl = '/hrm/employee';

        if (action == DetailPageAction.Close || action == DetailPageAction.SaveAndClose) {

            this.navigationService.navigateToReturnurl(this.router.url);
        }
        else if (action == DetailPageAction.SaveAndNew) {

            this.setEmployeeDatasource(JSON.parse(JSON.stringify(this.emptyEmployeeDataSource)));
            this.addressControl.clearAddressDataSource();

            this.navigationService.navigateAndUpdateReturnUrl(newNavigationUrl, this.router.url);
        }
        else if (action == DetailPageAction.Save && newNavigationUrl == this.router.url) {

            this.navigationService.navigateAndUpdateReturnUrl(newNavigationUrl + '/' + this.employeeId, this.router.url);
        }
        else {
            this.titlebar.setToolbarTitle("Employee: " + this.contactDataSource.firstName + " " + this.contactDataSource.lastName);
        }
    }
}
