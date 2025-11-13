import { Component, AfterViewInit, ViewChild, Inject, AfterContentChecked } from '@angular/core';
import { DxSelectBoxModule, DxTextAreaModule, DxDateBoxModule, DxFormModule, DxNumberBoxModule, DxTextBoxModule, DxCheckBoxModule, DxValidatorModule, DxValidationGroupComponent, DxColorBoxComponent, DxTreeListComponent, DxTreeListModule, DxFileUploaderComponent, DxValidatorComponent, DxSelectBoxComponent, DxTagBoxComponent, DxTextBoxComponent, DxDropDownBoxComponent, DxTreeViewComponent } from 'devextreme-angular';
import { MessageService } from './../../../../shared/services/message.service'
import { NavigationService } from './../../../../shared/services/navigation.service'
import { TitlebarComponent } from './../../../application-shared/components/titlebar/titlebar.component';
import { ImageUploadComponent } from './../../../application-shared/components/common/image-upload.component';
import { ActivatedRoute, Router, Params } from '@angular/router';
import { ToolbarType, DetailPageAction, PatterMatch, UserType } from './../../../application-shared/components/titlebar/utilities';
import { retry } from 'rxjs/operators';
import { RoleService } from './../../../system-settings/services/role.service';
import { UserService } from './../../../system-settings/services/user.service';
import DataSource from 'devextreme/data/data_source';
import CustomStore from "devextreme/data/custom_store";
import { HttpClient, HttpClientModule, HttpParams } from "@angular/common/http";
import { ToolbarItem, ToolbarItemOption } from '../../../application-shared/components/titlebar/toolbar-item';
import 'devextreme/integration/jquery';
import "rxjs/add/operator/toPromise";
declare var $: any;

@Component({
    selector: '.app-user-form',
    templateUrl: './user-form.component.html',
    styleUrls: ['./user-form.component.scss'],
})

export class UserFormComponent implements AfterViewInit, AfterContentChecked {

    roleDataSource: any = {};
    rightDataSource: any = [];
    treeListRoleInstance = undefined;
    treeListRightInstance = undefined;

    userDataSource: any = [];
    emptyUserDataSource: any = [];
    contactDataSource: any = [];
    photoDataSource: any = [];
    entityDataSource: any = [];

    businessProfileSelectItems: any = [];
    businessProfileDataSource: any = [];
    timeZoneSelectItems: any = [];
    authenticationTypeSelectItems: any = [];
    securityPolicySelectItems: any = [];
    titleSelectItems: any = [];
    userTypeSelectItems: any = [];
    businessProfileIds: any = [];

    isDefaultBusinessProfileUser: boolean = false;
    disabledBusinessProfileSelection = false;
    disabledUserTypeCombobox = false;
    fileUrl: any = "";
    selectedRowKeys: any[] = [];
    expandedRows: string[];
    filterValue: any = null;
    userId: string = "";
    password: string = "";
    expanded: boolean = false;
    toolbarAdditionalItems: ToolbarItem[];
    businessProfileId: any;
    emailPattern: any = PatterMatch.EmailPattern;
    contentClass: any = "detail-page-content-div";

    hideClass: string = "row form-group hide-remove-button";
    showClass: string = "row form-group show-remove-button";
    entityContentClass: any = this.showClass;

    entityName: any = "Contact";
    userType: any = UserType.Contact;

    isDropDownBoxOpened: boolean = false;
    gridDataSource: any = [];
    _gridBoxValue: number = 0;
    _gridSelectedRowKeys: number[] = [0];


    @ViewChild('roleTreeList')
    roleTreeList: DxTreeListComponent;

    @ViewChild(TitlebarComponent)
    private titlebar: TitlebarComponent;

    @ViewChild('passwordTextbox')
    private passwordTextbox: DxTextBoxComponent;

    @ViewChild('confirmedPasswordTextbox')
    private confirmedPasswordTextbox: DxTextBoxComponent;

    @ViewChild('imgUploadControl')
    private imageUploadControl: ImageUploadComponent;

    @ViewChild(DxTreeListComponent)
    private treeList: DxTreeListComponent;

    @ViewChild('formValidation')
    private formValidation: DxValidationGroupComponent;

    @ViewChild('businessProfileValidation')
    private businessProfileValidation: DxValidatorComponent;

    @ViewChild('firstNameValidation')
    private firstNameValidation: DxValidatorComponent;

    @ViewChild('lastNameValidation')
    private lastNameValidation: DxValidatorComponent;

    @ViewChild('emailValidation')
    private emailValidation: DxValidatorComponent;

    @ViewChild('timeZoneValidation')
    private timeZoneValidation: DxValidatorComponent;

    @ViewChild('authenticationValidation')
    private authenticationValidation: DxValidatorComponent;

    @ViewChild('shortUserNameValidation')
    private shortUserNameValidation: DxValidatorComponent;

    @ViewChild('passwordValidation')
    private passwordValidation: DxValidatorComponent;

    @ViewChild('confirmPasswordValidation')
    private confirmPasswordValidation: DxValidatorComponent;

    @ViewChild('securityPolicyValidation')
    private securityPolicyValidation: DxValidatorComponent;

    @ViewChild('entitySourceValidation')
    private entitySourceValidation: DxValidatorComponent;

    @ViewChild('userTypeValidation')
    private userTypeValidation: DxValidatorComponent;

    @ViewChild('userTypeCombobox')
    private userTypeSelectionBox: DxSelectBoxComponent;

    @ViewChild('entitySourceCombobox')
    private entitySourceSelectionBox: DxDropDownBoxComponent;

    @ViewChild('emailtextbox')
    private emailtextbox: DxTextBoxComponent;




    /*********************************************** Event Start ****************************************/

    /**
     * Constructor
     * @param roleService
     * @param userService
     * @param messageService
     * @param route
     * @param router
     */
    constructor(private roleService: RoleService,
        private userService: UserService,
        private messageService: MessageService,
        private navigationService: NavigationService,
        private route: ActivatedRoute,
        private router: Router) {

        this.route.params.subscribe(params => {
            if (params['userid'] !== undefined) {
                this.userId = params['userid'];
            }
        });

        this.passwordComparison = this.passwordComparison.bind(this);
        this.toolbarAdditionalItems = [];
    }

    public get isNewuser(): boolean {
        return this.userId == "" || this.userId == undefined || this.userId == null;
    }

    /**
     * Event
     **/
    ngAfterViewInit(): void {

        this.attachValidationToControl();
    }

    ngAfterContentChecked(): void {

        this.disabledUserTypeCombobox = !this.isNewuser;

        if (this.isNewuser) {
            var offset = new Date().getTimezoneOffset() * -1;
            var defaultTZ = this.timeZoneSelectItems.filter(x => x.tag == offset);
            if (defaultTZ.length > 0) {
                this.contactDataSource.timeZoneId = defaultTZ[0].id;
            }
        }
        else {

            this.userTypeSelectionBox.disabled = true;
            this.entityContentClass = this.hideClass;

            //this.passwordTextbox.disabled = true;
            //this.confirmedPasswordTextbox.disabled = true;
        }

        $('.dx-header-row').find('.dx-select-checkbox').hide();
    }

    /**
     * Event
     **/
    ngOnInit(): void {

        this.addAdditionalToolbar();
        $(".dx-fileuploader").find(".dx-button-content").css("background-color", "transparent!important");
        this.loadUserDetails();
    }

    /**
     * Event
     * @param e
     */
    onRoleTreelistInitialized(e) {

        this.treeListRoleInstance = e.component;
    }

    onRoleTreeListCellPrepared(e) {
        if (e.rowType === "data" && e.column.visibleIndex === 0 && e.data.disabled) {
            //&& e.data.City === "Los Angeles"            
            e.cellElement.find('.dx-select-checkbox').dxCheckBox("instance").option("disabled", true);
            e.cellElement.off();
        }
    }


    /*
     * on expand clicked
     **/
    onExpandClicked() {

        var keys: any[] = [];
        this.treeListRoleInstance.forEachNode(function (node) {
            keys.push(node.key);
        });

        for (var index in keys) {
            this.treeListRoleInstance.expandRow(keys[index]);
        }

        keys = [];
        this.treeListRightInstance.forEachNode(function (node) {
            keys.push(node.key);
        });

        for (var index in keys) {
            this.treeListRightInstance.expandRow(keys[index]);
        }
    }

    /*
     * on collapse clicked
     **/
    onCollapseClicked() {

        var keys: any[] = [];
        this.treeListRoleInstance.forEachNode(function (node) {
            keys.push(node.key);
        });

        for (var index in keys) {
            this.treeListRoleInstance.collapseRow(keys[index]);
        }

        keys = [];
        this.treeListRightInstance.forEachNode(function (node) {
            keys.push(node.key);
        });

        for (var index in keys) {
            this.treeListRightInstance.collapseRow(keys[index]);
        }
    }

    /**
     * Event
     * @param e
     */
    onRightTreelistInitialized(e) {
        this.treeListRightInstance = e.component;
    }

    /**
    * OnRowSelection event
    * @param e
    */
    onRowSelection(e) {


        var recordIds: any[] = e.selectedRowKeys;
        //loop through the selected roles
        //for (let item in selectedRoles) {
        //    recordIds.push(selectedRoles[item].toString());
        //}

        this.roleService.getPermissionListByRole(recordIds).subscribe(data => {
            this.rightDataSource = data.result;
        });


    }

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

    /**
     * on user type value changed
     * @param e
     */
    onUserTypeValueChanged(e): void {
        //if control is not disabled
        if (!this.userTypeSelectionBox.disabled) {

            if (e.value == UserType.Contact) {
                this.entityName = "Contact";
                this.userType = UserType.Contact;
                this.entityContentClass = this.showClass;
                this.entityDataSource = this.makeAsyncDataSource(this.businessProfileIds, e.value, this.userService);


            }
            else if (e.value == UserType.Employee) {
                this.entityName = "Employee";
                this.userType = UserType.Employee;
                this.entityContentClass = this.showClass;

                this.entityDataSource = this.makeAsyncDataSource(this.businessProfileIds, e.value, this.userService);

                //this.entitySourceSelectionBox.value = null;
                //this.contactDataSource = this.userDataSource.contact;
                //this.photoDataSource = this.userDataSource.contact.photo;
                //this.imageUploadControl.setEntityValue(this.photoDataSource);
                //this.emailtextbox.value = null;
            }
            else {
                this.entityContentClass = this.hideClass;
                this.contactDataSource = this.userDataSource.contact;
                this.photoDataSource = this.userDataSource.contact.photo;
                this.userType = UserType.User;
                this.imageUploadControl.setEntityValue(this.photoDataSource);
                this.entitySourceSelectionBox.value = null;
                this.emailtextbox.value = null;
            }

            //debugger;
            if (this.userId === "" || this.userId == null) {
                if (e.value == UserType.Contact || e.value == UserType.Employee) {
                    this.entitySourceValidation.validationRules = [{ type: 'required', message: this.entityName + ' is required.' }];
                }
                else {
                    this.entitySourceValidation.validationRules = [{ type: 'custom', validationCallback: this.validateCustomRule }];
                }
            }
        }
    }

    /************************************************ Method Start *********************************** */

    get gridBoxValue(): number {
        return this._gridBoxValue;
    }

    set gridBoxValue(value: number) {
        if (value == null) {
            this._gridSelectedRowKeys = [];
        }
        //this._gridSelectedRowKeys = value && [value] || [];
        this._gridBoxValue = value;
    }

    get gridSelectedRowKeys(): number[] {
        return this._gridSelectedRowKeys;
    }

    set gridSelectedRowKeys(value: number[]) {
        this._gridBoxValue = value.length && value[0] || null;
        this._gridSelectedRowKeys = value;
    }

    gridBox_displayExpr(item) {
        return item && item.name;
    }

    setValue(id: number): void {
        this._gridSelectedRowKeys.push(id);
    }

    getValue(): number {

        return this._gridSelectedRowKeys.length > 0 ? this._gridSelectedRowKeys[0] : 0;
    }



    onSelectionChanged(e) {

        this.isDropDownBoxOpened = false;
        if (this.userType == UserType.Contact) {
            this.setContactInformation();
        }
        else if (this.userType == UserType.Employee) {
            this.setContactInformation();
        }
    }

    setContactInformation(): void {

        if (this.getValue() > 0) {
            this.userService.getContactDetailForUserCreation(this.getValue()).subscribe(data => {
                this.contactDataSource = data.result.contactModel;
                this.photoDataSource = data.result.contactModel.photo;
                this.imageUploadControl.setEntityValue(this.photoDataSource);
                this.emailtextbox.value = this.contactDataSource.email;
            });
        }
    }

    /**
     * make data source
     * @param id
     * @param contactService
     */
    makeAsyncDataSource(ids: any[], userType: any, userService: UserService) {

        return new CustomStore({
            loadMode: "raw",
            key: "id",
            load: () => {
                return userService.getEntitiesDataSource(ids, userType).toPromise().then((response: any) => {
                    return response;
                })
            }
        });
    };

    /*
     * add additional menu item
     **/
    addAdditionalToolbar() {

        //expand menu
        var expandItem = new ToolbarItem();
        expandItem.location = 'after';
        expandItem.widget = 'dxButton';
        expandItem.locateInMenu = 'auto';
        expandItem.visible = true;
        expandItem.disabled = false;

        var expandItemOption = new ToolbarItemOption();
        expandItemOption.icon = 'chevrondown';
        expandItemOption.text = 'Expand';
        expandItemOption.onClick = () => {
            this.onExpandClicked();
        };
        expandItem.options = expandItemOption;
        this.toolbarAdditionalItems.push(expandItem);

        //collapse menu
        var collapseItem = new ToolbarItem();
        collapseItem.location = 'after';
        collapseItem.widget = 'dxButton';
        collapseItem.locateInMenu = 'auto';
        collapseItem.visible = true;
        collapseItem.disabled = false;

        var collapseItemOption = new ToolbarItemOption();
        collapseItemOption.icon = 'chevronup';
        collapseItemOption.text = 'Collapse';
        collapseItemOption.onClick = () => {
            this.onCollapseClicked();
        };
        collapseItem.options = collapseItemOption;
        this.toolbarAdditionalItems.push(collapseItem);

    }


    /**
     * attach validation to the controls
     *
     * */
    attachValidationToControl() {

        //{ type: 'email', message: 'Email is invalid' },

        //validation        
        this.businessProfileValidation.validationRules = [{ type: 'required', message: 'Business Profile is required.' }];
        this.firstNameValidation.validationRules = [{ type: 'required', message: 'First Name is required.' }];
        this.lastNameValidation.validationRules = [{ type: 'required', message: 'Last Name is required.' }];
        this.emailValidation.validationRules =
            [{ type: 'required', message: 'Email is required.' },
            { type: 'pattern', pattern: this.emailPattern, message: 'Email is invalid' }];
        this.timeZoneValidation.validationRules = [{ type: 'required', message: 'Time Zone is required.' }];
        this.authenticationValidation.validationRules = [{ type: 'required', message: 'Authentication Type is required.' }];
        this.shortUserNameValidation.validationRules = [{ type: 'required', message: 'Username is required.' }];
        this.securityPolicyValidation.validationRules = [{ type: 'required', message: 'Security Policy is required.' }];
        this.userTypeValidation.validationRules = [{ type: 'required', message: 'User type is required.' }];
        this.confirmPasswordValidation.validationRules =
            [ { type: 'compare', comparisonType: '==', comparisonTarget: this.passwordComparison, message: 'Password and Confirm Password do not match.' }];

        if (this.userId === "" || this.userId == null) {
            this.passwordValidation.validationRules = [{ type: 'required', message: 'Password is required.' }];
        }
    }

    validateCustomRule(): boolean {
        return true;
    }

    /**
     * password comparison
     * */
    passwordComparison() {
        return this.userDataSource.newPassword;
    };
    loadSelectboxDataSource(bpIds: number[]): void {

        //detail data source
        this.userService.getUserRoleAndTitleSelectItems(this.userId, bpIds).subscribe(data => {
            this.titleSelectItems = data.result.titleSelectItems;

            var defaultTitle = this.titleSelectItems.filter(x => x.isDefault);
            if (defaultTitle.length > 0) {
                this.contactDataSource.titleId = defaultTitle[0].id;
            }
            this.roleDataSource = data.result.userRoleSelectItems.result;
        });

        if (this.isNewuser) {

            let defaultItem = this.userTypeSelectItems.filter(x => x.isDefault)
            if (defaultItem.length > 0) {
                this.userDataSource.contactTypeId = defaultItem[0]['id'];
            }
            else {
                this.userDataSource.contactTypeId = null;
            }
        }

        if (!this.isNewuser && (this.userDataSource.contactTypeId == UserType.Contact || this.userDataSource.contactTypeId == UserType.Employee)) {
            this.entityDataSource = this.makeAsyncDataSource(this.businessProfileIds, this.userDataSource.contactTypeId, this.userService);
        }
    }
    /**
     * Init method
     **/
    loadUserDetails(): void {

        //detail data source
        this.userService.getUserDetailEntity(this.userId).subscribe(data => {

            this.isDefaultBusinessProfileUser = data.result.isDefaultBusinessProfile;

            this.authenticationTypeSelectItems = data.result.authenticationTypeSelectItems;
            //this.businessProfileSelectItems = data.result.businessProfileSelectItems;
            this.businessProfileDataSource = data.result.businessProfileSelectItems;
            this.timeZoneSelectItems = data.result.timeZoneSelectItems;
            this.securityPolicySelectItems = data.result.securityPolicySelectItems;
            this.userTypeSelectItems = data.result.userTypeSelectItems;
            //this.roleDataSource = data.result.userRoleSelectItems.result;

            this.disabledBusinessProfileSelection = data.result.disabledBusinessProfileSelection;
            this.emptyUserDataSource = data.result.emptyUser;
            this.setUserDataSource(data.result.user);

            this.titlebar.initializeToolbar(this.isNewuser ? "User : New" : "User : " + data.result.user.contact.firstName + " " + data.result.user.contact.lastName, this.toolbarAdditionalItems, ToolbarType.DetailPage);



        });
    }

    setUserDataSource(userModel: any): void {

        this.userDataSource = userModel;
        this.userId = userModel.id;
        this.contactDataSource = userModel.contact;
        if (this.isNewuser) {
            this.businessProfileSelectItems = JSON.parse(JSON.stringify(this.businessProfileDataSource));
        }
        else {

            var bps = this.businessProfileDataSource.filter(x => userModel.contact.businessProfileIds.includes(x.id));
            this.businessProfileSelectItems = bps;
        }
        this.businessProfileIds = userModel.businessProfileIds;

        this.photoDataSource = userModel.contact.photo;
        this.imageUploadControl.setEntityValue(this.photoDataSource);

    }

    /**
     * on value changed of business profile tag box
     * @param e
     */
    onBusinessProfileSelectionChanged(e): void {
      
      let newValue = e.value;
      if (newValue > 0) {
        this.loadSelectedBusinessProfileDomainNameById(newValue);
        this.loadSelectboxDataSource([newValue]);
      }
        //if (newValue.length > 0) {

       //  this.loadSelectboxDataSource(newValue);
        
        //}
    }
   /**
     * Business Profile Id
     * @param id
     */

    loadSelectedBusinessProfileDomainNameById(id) {
      var selectedBp = this.businessProfileSelectItems.find(x => x.id ==id);
      this.userDataSource.domainName = selectedBp.tag;
    }
    /**
     * 
     * @param closedWindow
     * @param isNew
     */
    saveEntity(action: DetailPageAction): void {


        if (this.userType == UserType.Contact) {

            this.userDataSource.contact = this.contactDataSource;
            this.userDataSource.contact.photo = this.imageUploadControl.photoDataSource;
        }
        else if (this.userType == UserType.Employee) {
            this.userDataSource.contact = this.contactDataSource;
            this.userDataSource.contact.photo = this.imageUploadControl.photoDataSource;
        }
        else {
            //get the photo data source to update child data source
            this.userDataSource.contact.photo = this.imageUploadControl.photoDataSource;
        }

        this.userDataSource.businessProfileIds = this.businessProfileIds;
        this.userDataSource.userRoles = this.roleTreeList.instance.getSelectedRowKeys("all");

        if (this.userId === "" || this.userId == null) {
            this.userService.createUser(this.userDataSource).subscribe(data => {
                this.userId = data.result;
                this.messageService.success("Record has been save successfully", 'Information');
                this.redirectToListPage(action);
            });
        } else {
            this.userService.updateUser(this.userId, this.userDataSource).subscribe(data => {
                this.messageService.success("Record has been save successfully", 'Information');
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

        this.saveEntity(action);
    }


    /**
    * redirect to list page
    */
    redirectToListPage(action: DetailPageAction): void {

        var newNavigationUrl = '/system-settings/user';


        if (action == DetailPageAction.Close || action == DetailPageAction.SaveAndClose) {

            this.navigationService.navigateToReturnurl(this.router.url);
        }
        else if (action == DetailPageAction.SaveAndNew) {

            this.setUserDataSource(JSON.parse(JSON.stringify(this.emptyUserDataSource)));
            this.rightDataSource = [];
            this.confirmedPasswordTextbox.value = '';

            this.navigationService.navigateAndUpdateReturnUrl(newNavigationUrl, this.router.url);
        }
        else if (action == DetailPageAction.Save && newNavigationUrl == this.router.url) {

            this.navigationService.navigateAndUpdateReturnUrl(newNavigationUrl + '/' + this.userId, this.router.url);
        }



    }




}
