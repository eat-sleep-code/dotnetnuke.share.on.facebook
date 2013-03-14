Imports DotNetNuke
Imports DotNetNuke.Common.Globals
Imports DotNetNuke.Entities.Modules
Imports DotNetNuke.Entities.Tabs
Imports DotNetNuke.Services.Exceptions

Namespace DONEIN_NET.Share_On_Facebook

    Public Class Settings
        Inherits DotNetNuke.Entities.Modules.PortalModuleBase
        Implements Entities.Modules.IActionable
        'Implements Entities.Modules.IPortable
        'Implements Entities.Modules.ISearchable



		#Region " Declare: Shared Classes "

		#End Region





		#Region " Declare: Local Objects "
			
			Protected WithEvents ddl_share_type As System.Web.UI.WebControls.DropDownList
			Protected WithEvents ddl_module_ID As System.Web.UI.WebControls.DropDownList
			Protected WithEvents ddl_page_ID As System.Web.UI.WebControls.DropDownList
			Protected WithEvents txt_url As System.Web.UI.WebControls.TextBox
			Protected WithEvents rad_link_type As System.Web.UI.WebControls.RadioButtonList
			Protected WithEvents txt_link_image As System.Web.UI.WebControls.TextBox
			Protected WithEvents txt_link_text As System.Web.UI.WebControls.TextBox
			Protected WithEvents txt_querystring As System.Web.UI.WebControls.TextBox
			
			Protected WithEvents btn_update As System.Web.UI.WebControls.LinkButton
			Protected WithEvents btn_cancel As System.Web.UI.WebControls.LinkButton

		#End Region
		
		
		
		

		#Region " Page: Load "

			Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
				
				If Not IsPostBack Then
				
					module_localize() '// LOCALIZE THE MODULE
					
					'// GET MODULE SETTINGS
					Dim tmp_facebook_share_type As String = CType(Settings("donein_facebook_share_type"), String)
					Dim tmp_facebook_module_ID As String = CType(Settings("donein_facebook_module_ID"), String)
					Dim tmp_facebook_page_ID As String = CType(Settings("donein_facebook_page_ID"), String)
					Dim tmp_facebook_file_ID As String = CType(Settings("donein_facebook_file_ID"), String)
					Dim tmp_facebook_url As String = CType(Settings("donein_facebook_url"), String)
					Dim tmp_facebook_link_type As String = CType(Settings("donein_facebook_link_type"), String)
					Dim tmp_facebook_link_image As String = CType(Settings("donein_facebook_link_image"), String)
					Dim tmp_facebook_link_text As String = CType(Settings("donein_facebook_link_text"), String)
					Dim tmp_facebook_querystring As String = CType(Settings("donein_facebook_querystring"), String)
					
					
					If tmp_facebook_share_type = "" Then
						ddl_share_type.SelectedValue = "0"
					Else
						ddl_share_type.SelectedValue = tmp_facebook_share_type
					End If
					
					
					'// GENERATE LIST OF MODULES FOR THIS TAB (PAGE)
					ddl_module_ID.Items.Clear
					ddl_module_ID.Items.Add(New ListItem("", "0"))
					For Each module_instance As ModuleInfo In PortalSettings.ActiveTab.Modules
						ddl_module_ID.Items.Add(New ListItem(module_instance.ModuleTitle, module_instance.ModuleID.ToString))
					Next
					If tmp_facebook_module_ID  = "" Then
						ddl_module_ID.SelectedIndex = 0
					Else
						Try
							ddl_module_ID.SelectedValue = tmp_facebook_module_ID 
						Catch ex As Exception '// MODULE MOVED OR DELETED
							ddl_module_ID.SelectedIndex = 0
						End Try
					End If
					
					
					'// GENERATE LIST OF TABS (PAGES) FOR THIS PORTAL
					ddl_page_ID.Items.Clear
					ddl_page_ID.Items.Add(New ListItem("", "0"))
					Dim tab_controller As New TabController
					For Each tab_instance As TabInfo In tab_controller.GetTabs(PortalSettings.PortalId)
						If (tab_instance.IsDeleted = False) And (tab_instance.IsAdminTab = False) And (tab_instance.IsSuperTab = False) And (Security.PortalSecurity.IsInRoles(tab_instance.AuthorizedRoles))
							ddl_page_ID.Items.Add(New ListItem(tab_instance.TabName, tab_instance.TabID.ToString))
						End If
					Next
					If tmp_facebook_page_ID  = "" Then
						ddl_page_ID.SelectedIndex = 0
					Else
						Try
							ddl_page_ID.SelectedValue = tmp_facebook_page_ID 
						Catch ex As Exception '// PAGE MOVED OR DELETED
							ddl_page_ID.SelectedIndex = 0
						End Try
					End If
					
					
'// TODO: ADD HANDLING FOR FILE SHARING HERE ======================================================================================================= /
					
					
					If tmp_facebook_url = "" Then
						txt_url.Text = ""
					Else
						txt_url.Text = tmp_facebook_url
					End If
					
					
					If tmp_facebook_link_type = "" Then
						rad_link_type.SelectedValue = "0"
					Else
						rad_link_type.SelectedValue = tmp_facebook_link_type
					End If
					
					
					If tmp_facebook_link_image = "" Then
						txt_link_image.Text = "/DesktopModules/DONEIN_NET/Share_On_Facebook/images/facebook_32.png"
					Else
						txt_link_image.Text = tmp_facebook_link_image
					End If
					
					
					If tmp_facebook_link_text = "" Then
						txt_link_text.Text = DotNetNuke.Services.Localization.Localization.GetString("pl_lnk_share.Text", LocalResourceFile)
					Else
						txt_link_text.Text = tmp_facebook_link_text
					End If
					
					
					If tmp_facebook_querystring = "" Then
						txt_querystring.Text = ""
					ElseIf tmp_facebook_querystring.Trim.StartsWith("?") = True Or tmp_facebook_querystring.Trim.StartsWith("&") = True Then
						txt_querystring.Text = Right(tmp_facebook_querystring.Trim, tmp_facebook_querystring.Trim.Length - 1).Trim
					Else
						txt_querystring.Text = tmp_facebook_querystring.Trim
					End If
					
					
					handle_share_type(ddl_share_type.SelectedValue)
					handle_link_type(rad_link_type.SelectedValue)
					
				End If
				
			End Sub

		#End Region



		
		
		#Region " Page: Localize "

 			Private Sub module_localize()
 				
				btn_update.Text = DotNetNuke.Services.Localization.Localization.GetString("pl_btn_update.Text", LocalResourceFile)
				btn_cancel.Text = DotNetNuke.Services.Localization.Localization.GetString("pl_btn_cancel.Text", LocalResourceFile)
				
				ddl_share_type.Items.Add(New ListItem(DotNetNuke.Services.Localization.Localization.GetString("pl_share_current_page.Text", LocalResourceFile), "0"))
				ddl_share_type.Items.Add(New ListItem(DotNetNuke.Services.Localization.Localization.GetString("pl_share_module.Text", LocalResourceFile), "1"))
				ddl_share_type.Items.Add(New ListItem(DotNetNuke.Services.Localization.Localization.GetString("pl_share_page.Text", LocalResourceFile), "2"))
				'ddl_share_type.Items.Add(New ListItem(DotNetNuke.Services.Localization.Localization.GetString("pl_share_file.Text", LocalResourceFile), "3"))
				ddl_share_type.Items.Add(New ListItem(DotNetNuke.Services.Localization.Localization.GetString("pl_share_url.Text", LocalResourceFile), "4"))
				
				rad_link_type.Items.Add(New ListItem(DotNetNuke.Services.Localization.Localization.GetString("pl_link_type_image.Text", LocalResourceFile), "1"))
				rad_link_type.Items.Add(New ListItem(DotNetNuke.Services.Localization.Localization.GetString("pl_link_type_text.Text", LocalResourceFile), "0"))
				
							
			End Sub 

		#End Region
			
			
			
			
		
		#Region " Handle: Field Dependencies "

			Private Sub ddl_share_type_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddl_share_type.SelectedIndexChanged
				handle_share_type(ddl_share_type.SelectedValue)
			End Sub
			
			
			
			Private Sub rad_link_type_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rad_link_type.SelectedIndexChanged
				handle_link_type(rad_link_type.SelectedValue)
			End Sub
		
		
		
			Public Sub handle_share_type(ByVal facebook_share_type As String)
				Select Case CInt(facebook_share_type)
					Case "4"
						'// URL
						ddl_module_ID.Enabled = False
						ddl_page_ID.Enabled = False
						txt_url.Enabled = True
						ddl_module_ID.Attributes.Add("CLASS", "field_disabled")
						ddl_page_ID.Attributes.Add("CLASS", "field_disabled")
						txt_url.Attributes.Add("CLASS", "field_enabled")
					Case "3"
						'// FILE (FUTURE)
						ddl_module_ID.Enabled = False
						ddl_page_ID.Enabled = False
						txt_url.Enabled = False
						ddl_module_ID.Attributes.Add("CLASS", "field_disabled")
						ddl_page_ID.Attributes.Add("CLASS", "field_disabled")
						txt_url.Attributes.Add("CLASS", "field_disabled")
					Case "2"
						'// OTHER PAGE 
						ddl_module_ID.Enabled = False
						ddl_page_ID.Enabled = True
						txt_url.Enabled = False
						ddl_module_ID.Attributes.Add("CLASS", "field_disabled")
						ddl_page_ID.Attributes.Add("CLASS", "field_enabled")
						txt_url.Attributes.Add("CLASS", "field_disabled")
					Case "1"
						'// MODULE
						ddl_module_ID.Enabled = True
						ddl_page_ID.Enabled = False
						txt_url.Enabled = False
						ddl_module_ID.Attributes.Add("CLASS", "field_enabled")
						ddl_page_ID.Attributes.Add("CLASS", "field_disabled")
						txt_url.Attributes.Add("CLASS", "field_disabled")
					Case Else
						'// CURRENT PAGE
						ddl_module_ID.Enabled = False
						ddl_page_ID.Enabled = False
						txt_url.Enabled = False
						ddl_module_ID.Attributes.Add("CLASS", "field_disabled")
						ddl_page_ID.Attributes.Add("CLASS", "field_disabled")
						txt_url.Attributes.Add("CLASS", "field_disabled")
				End Select
			End Sub
			
			
			
    		Public Sub handle_link_type(ByVal facebook_link_type As String)
				Select Case CInt(facebook_link_type)
					Case "1"
						txt_link_image.Enabled = True
						txt_link_text.Enabled = True
					Case Else
						txt_link_image.Enabled = True
						txt_link_text.Enabled = True
				End Select
			End Sub
			

		#End Region
		
		
		
		
		
		#Region " Handle: Update Button (btn_update) "

			Private Sub btn_update_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_update.Click
				Try
					Dim obj_modules As New ModuleController
					obj_modules.UpdateModuleSetting(ModuleId, "donein_facebook_share_type", ddl_share_type.SelectedValue)
					obj_modules.UpdateModuleSetting(ModuleId, "donein_facebook_module_ID", ddl_module_ID.SelectedValue)
					obj_modules.UpdateModuleSetting(ModuleId, "donein_facebook_page_ID", ddl_page_ID.SelectedValue)
					obj_modules.UpdateModuleSetting(ModuleId, "donein_facebook_file_ID", "0")
					obj_modules.UpdateModuleSetting(ModuleId, "donein_facebook_url", txt_url.Text.Trim)
					obj_modules.UpdateModuleSetting(ModuleId, "donein_facebook_link_type", rad_link_type.SelectedValue)
					obj_modules.UpdateModuleSetting(ModuleId, "donein_facebook_link_image", txt_link_image.Text.Trim)
					obj_modules.UpdateModuleSetting(ModuleId, "donein_facebook_link_text", txt_link_text.Text.Trim)
					If txt_querystring.Text.Trim.StartsWith("?") = True Or txt_querystring.Text.Trim.StartsWith("&") = True Then
						obj_modules.UpdateModuleSetting(ModuleId, "donein_facebook_querystring", Right(txt_querystring.Text.Trim, txt_querystring.Text.Trim.Length - 1).Trim)
					Else
						obj_modules.UpdateModuleSetting(ModuleId, "donein_facebook_querystring", txt_querystring.Text.Trim)
					End If
					
				
					Response.Redirect(NavigateURL(), True)
				Catch ex As Exception 
					ProcessModuleLoadException(Me, ex)
				End Try
			End Sub
			
		#End Region
		
		
		
		
		
		#Region " Handle: Cancel Button (btn_cancel) "

			Private Sub btn_cancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_cancel.Click
				Try
					Response.Redirect(NavigateURL(), True)
				Catch ex As Exception		  
					ProcessModuleLoadException(Me, ex)
				End Try
			End Sub	
			
		#End Region		



	
	
		#Region " Web Form Designer Generated Code "

				'This call is required by the Web Form Designer.
				<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

				End Sub

				Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
					'CODEGEN: This method call is required by the Web Form Designer
					'Do not modify it using the code editor.
					InitializeComponent()
				End Sub

		#End Region

		
		
		
		
		#Region "Optional Interfaces"

			Public ReadOnly Property ModuleActions() As Entities.Modules.Actions.ModuleActionCollection Implements Entities.Modules.IActionable.ModuleActions
				Get
					Dim Actions As New Entities.Modules.Actions.ModuleActionCollection
					'// DO NOTHING
					Return Actions
				End Get
			End Property

			'Public Function ExportModule(ByVal ModuleID As Integer) As String Implements Entities.Modules.IPortable.ExportModule
			'	' included as a stub only so that the core knows this module Implements Entities.Modules.IPortable
			'End Function

			'Public Sub ImportModule(ByVal ModuleID As Integer, ByVal Content As String, ByVal Version As String, ByVal UserId As Integer) Implements Entities.Modules.IPortable.ImportModule
			'	' included as a stub only so that the core knows this module Implements Entities.Modules.IPortable
			'End Sub

			'Public Function GetSearchItems(ByVal ModInfo As Entities.Modules.ModuleInfo) As Services.Search.SearchItemInfoCollection Implements Entities.Modules.ISearchable.GetSearchItems
			'	' included as a stub only so that the core knows this module Implements Entities.Modules.ISearchable
			'End Function

		#End Region
		
		
		
	
    	
    End Class

End Namespace