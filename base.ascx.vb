Imports DotNetNuke
Imports DotNetNuke.Common.Globals
Imports DotNetNuke.Entities.Modules
Imports DotNetNuke.Entities.Tabs
Imports DotNetNuke.Services.FileSystem

Namespace DONEIN_NET.Share_On_Facebook

	Public Class Base
		Inherits DotNetNuke.Entities.Modules.PortalModuleBase
		Implements Entities.Modules.IActionable
        'Implements Entities.Modules.IPortable
        Implements Entities.Modules.ISearchable
		
		
		
		#Region " Declare: Shared Classes "

			Private module_info As New Module_Info()
			
		#End Region





		#Region " Declare: Local Objects "
			Protected WithEvents lbl_message As System.Web.UI.WebControls.Label
			Protected WithEvents lnk_share As System.Web.UI.WebControls.HyperLink
				
		#End Region





		#Region " Page: Load "

			Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
				If Request.QueryString.Item("debug") <> "" Then
					module_info.get_info(Request.QueryString.Item("debug").Trim, ModuleID, TabID)
				End If
				
				'// GET MODULE SETTINGS
				Dim tmp_facebook_share_type As Integer = 0
				Dim tmp_facebook_module_ID  As Integer = 0
				Dim tmp_facebook_page_ID  As Integer = 0
				Dim tmp_facebook_file_ID  As Integer = 0
				Dim tmp_facebook_url As String = ""
				Dim tmp_facebook_link_type As Integer = 0 
				Dim tmp_facebook_link_image As String = ""
				Dim tmp_facebook_link_text As String = ""
				Dim tmp_facebook_querystring As String = ""
								
				Try
					tmp_facebook_share_type = CType(Settings("donein_facebook_share_type"), Integer)
					tmp_facebook_module_ID = CType(Settings("donein_facebook_module_ID"), Integer)
					tmp_facebook_page_ID = CType(Settings("donein_facebook_page_ID"), Integer)
					tmp_facebook_file_ID = CType(Settings("donein_facebook_file_ID"), Integer)
					tmp_facebook_url = CType(Settings("donein_facebook_url"), String)
					tmp_facebook_link_type = CType(Settings("donein_facebook_link_type"), Integer)
					tmp_facebook_link_image = CType(Settings("donein_facebook_link_image"), String)
					tmp_facebook_link_text = CType(Settings("donein_facebook_link_text"), String)
					tmp_facebook_querystring = CType(Settings("donein_facebook_querystring"), String)
				Catch ex As Exception
					Response.Write("Error Reading Database Values<BR />")
				End Try
				
				'// CREATE LINK ==============================================================================================================================================
				Dim tmp_link_url As String = Request.Url.AbsoluteUri
				Dim tmp_link_text As String = ""
				Dim tmp_url_base As String = ""
				Try
					tmp_url_base = Request.Url.AbsoluteUri.Replace(Request.Url.Query, "")
				Catch ex As Exception
					tmp_url_base = Request.Url.AbsoluteUri.ToString
				End Try
				
				If tmp_facebook_share_type = 4 Then
					'// SHARE ANOTHER URL
					tmp_link_url = append_querystring(tmp_facebook_url, tmp_facebook_querystring)
					tmp_link_text = ""

				Else If tmp_facebook_share_type = 3 Then
					'// SHARE A FILE ON THE SITE
					Dim file_controller As New FileController
					Dim file_info As FileInfo = file_controller.GetFileById(tmp_facebook_file_ID, PortalSettings.PortalId)
					tmp_link_url = append_querystring(PortalSettings.PortalAlias.HTTPAlias + file_info.Folder + file_info.FileName, tmp_facebook_querystring)
					tmp_link_text = ""

				ElseIf  tmp_facebook_share_type = 2 Then
					'// SHARE A PAGE ON THE SITE (OTHER THAN THE CURRENT ONE)	
					Dim tab_controller As New TabController
					If Common.HostSettings.Item("UseFriendlyUrls").ToString = "Y" Then
						tmp_link_url = append_querystring(FriendlyUrl(tab_controller.GetTab(tmp_facebook_page_ID), ApplicationURL(tmp_facebook_page_ID)), tmp_facebook_querystring)
					Else
						tmp_link_url = append_querystring((tmp_url_base.ToLower + ApplicationURL(tmp_facebook_page_ID.ToString)).Replace("default.aspx~/", ""), tmp_facebook_querystring)
					End If
					Try
						tmp_link_text = tab_controller.GetTab(tmp_facebook_page_ID).TabName
					Catch
						tmp_link_text = PortalSettings.PortalName
					End Try
					
				Else If tmp_facebook_share_type = 1 And tmp_facebook_module_ID > 0 Then
					'// SHARE ONLY MODULE
					Dim module_controller As New ModuleController
					If Common.HostSettings.Item("UseFriendlyUrls").ToString = "Y" Then
						tmp_link_url = append_querystring(FriendlyUrl(PortalSettings.ActiveTab, ApplicationURL(PortalSettings.ActiveTab.TabID)).Replace("/Default.aspx", "/mid/" + tmp_facebook_module_ID.ToString + "/Default.aspx"), tmp_facebook_querystring)
					Else
						tmp_link_url = append_querystring((tmp_url_base.ToLower + ApplicationURL(PortalSettings.ActiveTab.TabID.ToString) + "&mid=" + tmp_facebook_module_ID.ToString).Replace("default.aspx~/", ""), tmp_facebook_querystring)
					End If
					Try
						tmp_link_text = module_controller.GetModule(tmp_facebook_module_ID, PortalSettings.ActiveTab.TabID).ModuleTitle
					Catch
						tmp_link_text = PortalSettings.ActiveTab.TabName
					End Try

				Else If tmp_facebook_share_type = 0 Then
					'// SHARE CURRENT PAGE
					If Common.HostSettings.Item("UseFriendlyUrls").ToString = "Y" Then
						tmp_link_url = append_querystring(FriendlyUrl(PortalSettings.ActiveTab, ApplicationURL(PortalSettings.ActiveTab.TabID)), tmp_facebook_querystring)
					Else
						tmp_link_url = append_querystring((tmp_url_base.ToLower + ApplicationURL(PortalSettings.ActiveTab.TabID.ToString)).Replace("default.aspx~/", ""), tmp_facebook_querystring)
					End If
					tmp_link_text = PortalSettings.ActiveTab.TabName

				End If

				If tmp_link_text.Trim.Length = 0 Then
					lnk_share.NavigateUrl="javascript: var share_window = window.open('http://www.facebook.com/sharer.php?u='+encodeURIComponent('" + tmp_link_url + "'),'facebook_share','toolbar=no,width=642,height=436');"
				Else
					lnk_share.NavigateUrl="javascript: var share_window = window.open('http://www.facebook.com/sharer.php?u='+encodeURIComponent('" + tmp_link_url + "')+'&t='+encodeURIComponent('" + tmp_link_text + "'),'facebook_share','toolbar=no,width=642,height=436');"
				End If
				


				'// CREATE TEXT OR IMAGE BUTTON ==============================================================================================================================

				If tmp_facebook_link_type = 1 And (tmp_facebook_link_image + "").Trim.Length > 0 Then
					'// IMAGE LINK
					lnk_share.Text = "<IMG ALT=""" + tmp_facebook_link_text + """ BORDER=""0"" SRC=""" + DotNetNuke.Common.ApplicationPath + tmp_facebook_link_image + """ TITLE=""" + DotNetNuke.Services.Localization.Localization.GetString("pl_lnk_share.Tooltip", LocalResourceFile) + """ CLASS=""donein_facebook_image"">"
				Else
					'// TEXT LINK
					lnk_share.Text = tmp_facebook_link_text
				End If
				
				
			End Sub

		#End Region
		
		
	
		#Region " Handle: QueryString Appendage "
			Public Function append_querystring(ByVal url_input As String, Optional ByVal querystring_to_append As String = "") As String
				Dim url_output As String = ""
				If Not querystring_to_append Is Nothing
					If querystring_to_append.Trim.Length > 0 Then
						If Common.HostSettings.Item("UseFriendlyUrls").ToString = "Y" Then
							querystring_to_append = querystring_to_append.Replace("?", "/").Replace("&", "/").Replace("=", "/")
							url_output = url_input.Replace("/Default.aspx", "/" + querystring_to_append + "/Default.aspx")
						Else
							If url_input.Contains("?") = True Then
								url_output = url_input + "&" + querystring_to_append
							Else
								url_output = url_input + "?" + querystring_to_append
							End If
						End If
					Else
						url_output = url_input
					End If
				Else
					url_output = url_input
				End If
				
				Return url_output
			End Function
		#End Region



		#Region " Page: PreRender "

			Private Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.PreRender
				module_localize() '// LOCALIZE THE MODULE
			End Sub

		#End Region





		#Region " Page: Localization "

 			Private Sub module_localize()
 				Try
					lnk_share.ToolTip = DotNetNuke.Services.Localization.Localization.GetString("pl_lnk_share.Tooltip", LocalResourceFile)
					If lnk_share.Text.Trim.Length = 0 Then
						lnk_share.Text = DotNetNuke.Services.Localization.Localization.GetString("pl_lnk_share.Text", LocalResourceFile)
					End If
				Catch ex As Exception
					Response.Write("Error Reading Localization File<BR />")
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
		
		
		
		

		#Region " Optional Interfaces "

			Public ReadOnly Property ModuleActions() As Entities.Modules.Actions.ModuleActionCollection Implements Entities.Modules.IActionable.ModuleActions
				Get
					Dim Actions As New Entities.Modules.Actions.ModuleActionCollection
						Actions.Add(GetNextActionID, DotNetNuke.Services.Localization.Localization.GetString(Entities.Modules.Actions.ModuleActionType.ContentOptions, LocalResourceFile), Entities.Modules.Actions.ModuleActionType.ContentOptions, "", "", EditUrl("Edit"), False, Security.SecurityAccessLevel.Edit, True, False)
					Return Actions
				End Get
			End Property


			'Public Function ExportModule(ByVal ModuleID As Integer) As String Implements Entities.Modules.IPortable.ExportModule
			'	' included as a stub only so that the core knows this module Implements Entities.Modules.IPortable
			'End Function

			'Public Sub ImportModule(ByVal ModuleID As Integer, ByVal Content As String, ByVal Version As String, ByVal UserId As Integer) Implements Entities.Modules.IPortable.ImportModule
			'	' included as a stub only so that the core knows this module Implements Entities.Modules.IPortable
			'End Sub

			Public Function GetSearchItems(ByVal ModInfo As Entities.Modules.ModuleInfo) As Services.Search.SearchItemInfoCollection Implements Entities.Modules.ISearchable.GetSearchItems
				' included as a stub only so that the core knows this module Implements Entities.Modules.ISearchable
			End Function

		#End Region
		
		
  		
  End Class

End Namespace
