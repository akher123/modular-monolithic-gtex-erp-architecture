import { Component, AfterViewInit, ViewChild, Inject, Input, AfterContentChecked } from '@angular/core';
import { DxSelectBoxModule, DxTextAreaModule, DxDateBoxModule, DxFormModule, DxNumberBoxModule, DxTextBoxModule, DxCheckBoxModule, DxValidatorModule, DxValidationGroupComponent, DxColorBoxComponent, DxTreeListComponent, DxTreeListModule, DxFileUploaderComponent, DxValidatorComponent, DxSelectBoxComponent, DxTextAreaComponent, DxTagBoxComponent, DxRadioGroupComponent, DxDropDownBoxComponent, DxDataGridComponent } from 'devextreme-angular';
import { MessageService } from './../../../../shared/services/message.service'
import { TitlebarComponent } from './../../../application-shared/components/titlebar/titlebar.component';
import { ImageUploadComponent } from './../../../application-shared/components/common/image-upload.component';
import { ActivatedRoute, Router, Params } from '@angular/router';
import { ToolbarType, DetailPageAction, PatterMatch } from './../../../application-shared/components/titlebar/utilities';
import { retry } from 'rxjs/operators';
import { RoleService } from './../../../system-settings/services/role.service';
import { UserService } from './../../../system-settings/services/user.service';
import DataSource from 'devextreme/data/data_source';
import CustomStore from "devextreme/data/custom_store";
import { HttpClient, HttpClientModule, HttpParams } from "@angular/common/http";
import { ToolbarItem, ToolbarItemOption } from '../../../application-shared/components/titlebar/toolbar-item';

import { ContactService } from '../../services/contact.service';
import { NavigationService } from './../../../../shared/services/navigation.service';
import "rxjs/add/operator/toPromise";
import { InlineAddressComponent } from './../../../system-service/components/address/inline-address.component';
declare var $: any;

@Component({
    selector: 'app-contact-form',
    templateUrl: './contact-form.component.html',
    styleUrls: ['./contact-form.component.scss']
})

export class ContactFormComponent implements AfterContentChecked {

    //@Input() entityModel: EntityModel = new EntityModel();

    @Input('ContactId') contactId: number = 0;
    toolbarType: ToolbarType = ToolbarType.DetailPage;
    minDate: Date = new Date(1900, 0, 1);
    nowDate: Date = new Date();
    maxDate: Date = new Date(this.nowDate.getFullYear() - 18, this.nowDate.getMonth(), this.nowDate.getDate());
    emailPattern: any = PatterMatch.EmailPattern;
    contentClass: any = "detail-page-content-div";

    contactDataSource: any = [];
    emptyContactDataSource: any = [];
    photoDataSource: any = [];

    businessProfileSelectItems: any = [];
    companySelectItems: any = [];
    titleSelectItems: any = [];
    positionSelectItems: any = [];
    timeZoneSelectItems: any = [];
    skillsSelectItems: any = [];
    imTypeSelectItems: any = [];
    preferredContactSelectItems: any = [];
    genderSelectItems: any = [];
    preferredPhoneTypeSelectItems: any = [];
    isDefaultBusinessProfileUser: boolean = false;
    disabledBusinessProfileUser = true;

    companySelectedValue: any;

    contactSpecialisations: any = [];
    companyIds: any = [];
    businessProfileIds: any = [];

    preferredPhoneTypeSelectItem1: any = [];
    preferredPhoneTypeSelectItem2: any = [];
    preferredPhoneTypeSelectItem3: any = [];

    showEmail2Field: boolean = false;
    showEmail3Field: boolean = false;

    showIM2Field: boolean = false;
    showIM3Field: boolean = false;

    //_companyValue: number[] = [21];    
    _gridSelectedRowKeys: number[] = [];
    gridDataSource: any;

    isDropDownBoxOpened = false;
    toolbarAdditionalItems: ToolbarItem[];

    @ViewChild(TitlebarComponent)
    private titlebar: TitlebarComponent;

    @ViewChild('imgUploadControl')
    private imageUploadControl: ImageUploadComponent;

    @ViewChild('addressControl')
    private addressControl: InlineAddressComponent;


    @ViewChild('formValidation')
    private formValidation: DxValidationGroupComponent;

    @ViewChild('gridContainer')
    private gridContainer: DxDataGridComponent;

    @ViewChild('companyCombobox')
    private companyCombobox: DxDropDownBoxComponent;

    @ViewChild('businessProfileSelectionBox')
    private businessProfileSelectionBox: DxSelectBoxComponent;



    @ViewChild('businessProfileValidation')
    private businessProfileValidation: DxValidatorComponent;

    //@ViewChild('companyValidation')
    //private companyValidation: DxValidatorComponent;

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




    /*********************************************** Event Start ****************************************/

    /**
     * Constructor
     * @param roleService
     * @param userService
     * @param messageService
     * @param route
     * @param router
     */
    constructor(private contactService: ContactService,
        private messageService: MessageService,
        private navigationService: NavigationService,
        private route: ActivatedRoute,
        private router: Router) {


        this.route.params.subscribe(params => {
            if (params['contactId'] !== undefined) {
                this.contactId = params['contactId'];
                this.toolbarType = ToolbarType.DetailTabPage;
                this.contentClass = "detail-page-content-div-tab";
            }
        });
    }

    /**
     * Event
     **/
    ngAfterContentChecked(): void {

        this.disabledBusinessProfileUser = this.contactId > 0 || !this.isDefaultBusinessProfileUser;

        if (this.contactId == 0) {

            var offset = new Date().getTimezoneOffset() * -1;
            var defaultTZ = this.timeZoneSelectItems.filter(x => x.tag == offset);
            if (defaultTZ.length > 0) {
                this.contactDataSource.timeZoneId = defaultTZ[0].id;
            } 
        }
    }
    /**
     * Event
     **/
    ngOnInit(): void {

        $(".dx-fileuploader").find(".dx-button-content").css("background-color", "transparent!important");
        this.init();
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
     * on value changed of business profile tag box
     * @param e
     */
    onBusinessProfileSelectionChanged(e): void {


        let newValue = e.value;
        if (newValue.length > 0)
            this.populateCompanyOnBusinessProfileSelectionChanged(newValue);

        this.loadSelectBoxDataByBP(newValue);
        this.addressControl.LoadAddressTypeSelectBoxByBpIds(newValue);
    }

    /**
     * Populate company on business profile selection changed
     * @param id
     */
    populateCompanyOnBusinessProfileSelectionChanged(ids: any[]) {
        this.gridDataSource = this.makeAsyncDataSource(ids, this.contactService);
    }

    get gridBoxValue(): number[] {
        return this.companyIds;
    }

    set gridBoxValue(value: number[]) {

        this.companyIds = value || [];
    }

    /**
     * make data source
     * @param id
     * @param contactService
     */
    makeAsyncDataSource(ids: any[], contactService: ContactService) {
        return new CustomStore({
            loadMode: "raw",
            key: "id",
            load: () => {
                return contactService.getCompanyByBusinessProfile(ids).toPromise().then((response: any) => {
                    //set company value
                    if (this.contactDataSource != undefined) {
                        this.setCompanyValue(this.contactDataSource.companyIds);
                    }
                    return response.result;
                })
            }
        });
    };

    /**
     * on grid dropdown selection changed
     * @param e
     */
    onSelectionChanged(e) {
        //this.isDropDownBoxOpened = false;
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

        this.emailValidation.validationRules = [{ type: 'required', message: 'Email is required.' },
        { type: 'pattern', pattern: this.emailPattern, message: 'Email is invalid' }];
        //this.email2Validation.validationRules = [{ type: 'pattern', pattern: this.emailPattern, message: 'Email 2 is invalid' }];        
        //this.email3Validation.validationRules = [{ type: 'pattern', pattern: this.emailPattern, message: 'Email 3 is invalid' }];        
    }


    /**
     * Init method
     **/
    init(): void {

        this.contactService.getContact(this.contactId).toPromise().then((response: any) => {

            if (response == undefined || response == null) return;

            this.isDefaultBusinessProfileUser = response.result.isDefaultBusinessProfile;
            this.emptyContactDataSource = response.result.emptyContactModel;

            this.setContactDataSource(response.result.contactModel);

            this.businessProfileSelectItems = response.result.businessProfileSelectItems;           
            this.timeZoneSelectItems = response.result.timezoneSelectItems;



            this.titlebar.initializeToolbar(this.contactId == 0 ? "Contact: New" : "Contact: " + this.contactDataSource.firstName + " " + this.contactDataSource.lastName, null, this.toolbarType);

            this.preferredPhoneTypeSelectItem1 = [];
            this.preferredPhoneTypeSelectItem2 = [];
            this.preferredPhoneTypeSelectItem3 = [];
            this.preferredPhoneTypeSelectItem1.push(response.result.preferredPhoneTypeSelectItems[0]);
            this.preferredPhoneTypeSelectItem2.push(response.result.preferredPhoneTypeSelectItems[1]);
            this.preferredPhoneTypeSelectItem3.push(response.result.preferredPhoneTypeSelectItems[2]);
             
        });

    }

    setContactDataSource(contactModel: any): void {
       
        this.contactDataSource = contactModel;
        this.contactId = contactModel.id;
        this.contactSpecialisations = this.contactDataSource.contactSpecialisationIds;
        this.companyIds = this.contactDataSource.companyIds;
        this.businessProfileIds = this.contactDataSource.businessProfileIds;
        this.photoDataSource = this.contactDataSource.photo;
        this.imageUploadControl.setEntityValue(this.photoDataSource);

        this.showEmail2Field = this.contactDataSource.email2 != null && this.contactDataSource.email2 != '';
        this.showEmail3Field = this.contactDataSource.email3 != null && this.contactDataSource.email3 != '';

        this.showIM2Field = this.contactDataSource.imLoginId2 != null && this.contactDataSource.imLoginId2 != '';
        this.showIM3Field = this.contactDataSource.imLoginId3 != null && this.contactDataSource.imLoginId3 != '';
    }

    loadSelectBoxDataByBP(businessProfileIds: number[]) {
        this.contactService.getContactSelectBoxItems(businessProfileIds).toPromise().then((response: any) => {
            this.imTypeSelectItems = response.result.imTypeSelectItems;
            this.titleSelectItems = response.result.titleSelectItems;
            this.positionSelectItems = response.result.positionSelectItems;
            this.skillsSelectItems = response.result.skillsSelectItems;
            this.genderSelectItems = response.result.genderSelectItems;
            this.preferredContactSelectItems = response.result.preferredContactMethodSelectItems;
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
        this.contactDataSource.businessProfileIds = this.businessProfileIds;
        this.contactDataSource.companyIds = this.companyIds;

        if (this._gridSelectedRowKeys[0] != undefined)
            this.contactDataSource.companyId = this._gridSelectedRowKeys[0];

        if (this.contactId == 0) {

            this.contactDataSource.contactAddresses = this.addressControl.getAddressesFromMemory()

            this.contactService.createContact(this.imageUploadControl.fileName, this.contactDataSource).subscribe(data => {
                this.messageService.success("Record has been saved successfully", 'Information');
                this.contactId = data.result;
                this.contactDataSource.id = data.result;

                this.redirectToListPage(action);
            });
        }
        else {
            this.contactService.updateContact(this.imageUploadControl.fileName, this.contactDataSource).subscribe(data => {
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

        var newNavigationUrl = '/crm/contact';

        if (action == DetailPageAction.Close || action == DetailPageAction.SaveAndClose) {

            this.navigationService.navigateToReturnurl(this.router.url);
        }
        else if (action == DetailPageAction.SaveAndNew) {
            this.setContactDataSource(JSON.parse(JSON.stringify(this.emptyContactDataSource)));
            this.addressControl.clearAddressDataSource();

            this.navigationService.navigateAndUpdateReturnUrl(newNavigationUrl, this.router.url);
        }
        else if (action == DetailPageAction.Save && newNavigationUrl == this.router.url) {

            this.navigationService.navigateAndUpdateReturnUrl(newNavigationUrl + '/' + this.contactId, this.router.url);
        }
        else {
            this.titlebar.setToolbarTitle("Contact: " + this.contactDataSource.firstName + " " + this.contactDataSource.lastName);
        }

    }
}
