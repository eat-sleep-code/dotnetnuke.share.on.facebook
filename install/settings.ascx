<%@ Control Language="vb" AutoEventWireup="false" Codebehind="settings.ascx.vb" Inherits="DONEIN_NET.Share_On_Facebook.Settings" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>

<TABLE BORDER="0" CELLSPACING="0" CELLPADDING="2" ALIGN="left">
	<TR HEIGHT="30">
		<TD WIDTH="120" CLASS="SubHead" ALIGN="left" VALIGN="middle">
			<DNN:LABEL RUNAT="server" ID="pl_share_type" CONTROLNAME="ddl_share_type" SUFFIX=":" />
		</TD>
		<TD WIDTH="480" ALIGN="left" VALIGN="middle">
			<ASP:DROPDOWNLIST RUNAT="server" ID="ddl_share_type" CSSCLASS="NormalTextBox" STYLE="width: 400px;" AUTOPOSTBACK="true" />
		</TD>
	</TR>
	
	<TR HEIGHT="30">
		<TD WIDTH="120" CLASS="SubHead" ALIGN="left" VALIGN="middle">
			<DNN:LABEL RUNAT="server" ID="pl_module_ID" CONTROLNAME="ddl_module_ID" SUFFIX=":" />
		</TD>
		<TD WIDTH="480" ALIGN="left" VALIGN="middle">
			<ASP:DROPDOWNLIST RUNAT="server" ID="ddl_module_ID" CSSCLASS="NormalTextBox" STYLE="width: 400px;"/>
		</TD>
	</TR>
	
	<TR HEIGHT="30">
		<TD WIDTH="120" CLASS="SubHead" ALIGN="left" VALIGN="middle">
			<DNN:LABEL RUNAT="server" ID="pl_page_ID" CONTROLNAME="ddl_page_ID" SUFFIX=":" />
		</TD>
		<TD WIDTH="480" ALIGN="left" VALIGN="middle">
			<ASP:DROPDOWNLIST RUNAT="server" ID="ddl_page_ID" CSSCLASS="NormalTextBox" STYLE="width: 400px;"/>
		</TD>
	</TR>
	
	<TR HEIGHT="30">
		<TD WIDTH="120" CLASS="SubHead" ALIGN="left" VALIGN="middle">
			<DNN:LABEL RUNAT="server" ID="pl_url" CONTROLNAME="txt_url" SUFFIX=":" />
		</TD>
		<TD WIDTH="480" ALIGN="left" VALIGN="middle">
			<ASP:TEXTBOX RUNAT="server" ID="txt_url" CSSCLASS="NormalTextBox" MAXLENGTH="1024" STYLE="width: 400px;" />
		</TD>
	</TR>
	
	
	<TR HEIGHT="30">
		<TD COLSPAN="2" ALIGN="center" VALIGN="middle">
			<HR NOSHADE />
		</TD>
	</TR>
	
	<TR HEIGHT="30">
		<TD WIDTH="120" CLASS="SubHead" ALIGN="left" VALIGN="middle">
			<DNN:LABEL RUNAT="server" ID="pl_link_type" CONTROLNAME="rad_link_type" SUFFIX=":" />
		</TD>
		<TD WIDTH="480" ALIGN="left" VALIGN="middle">
			<ASP:RADIOBUTTONLIST RUNAT="server" ID="rad_link_type" CSSCLASS="NormalTextBox" REPEATDIRECTION="Horizontal" AUTOPOSTBACK="true" />
		</TD>
	</TR>
	
	<TR HEIGHT="30">
		<TD WIDTH="120" CLASS="SubHead" ALIGN="left" VALIGN="middle">
			<DNN:LABEL RUNAT="server" ID="pl_link_image" CONTROLNAME="txt_link_image" SUFFIX=":" />
		</TD>
		<TD WIDTH="480" ALIGN="left" VALIGN="middle">
			<ASP:TEXTBOX RUNAT="server" ID="txt_link_image" CSSCLASS="NormalTextBox" MAXLENGTH="255"  STYLE="width: 400px;" />
		</TD>
	</TR>
	
	<TR HEIGHT="30">
		<TD WIDTH="120" CLASS="SubHead" ALIGN="left" VALIGN="middle">
			<DNN:LABEL RUNAT="server" ID="pl_link_text" CONTROLNAME="txt_link_text" SUFFIX=":" />
		</TD>
		<TD WIDTH="480" ALIGN="left" VALIGN="middle">
			<ASP:TEXTBOX RUNAT="server" ID="txt_link_text" CSSCLASS="NormalTextBox" MAXLENGTH="255"  STYLE="width: 400px;" />
		</TD>
	</TR>
	
	
	<TR HEIGHT="30">
		<TD COLSPAN="2" ALIGN="center" VALIGN="middle">
			<HR NOSHADE />
		</TD>
	</TR>
	
	<TR HEIGHT="30">
		<TD WIDTH="120" CLASS="SubHead" ALIGN="left" VALIGN="middle">
			<DNN:LABEL RUNAT="server" ID="pl_querystring" CONTROLNAME="txt_querystring" SUFFIX=":" />
		</TD>
		<TD WIDTH="480" ALIGN="left" VALIGN="middle">
			<ASP:TEXTBOX RUNAT="server" ID="txt_querystring" CSSCLASS="NormalTextBox" MAXLENGTH="1024"  STYLE="width: 400px;" />
		</TD>
	</TR>
	
	<TR HEIGHT="30">
		<TD COLSPAN="2" ALIGN="center" VALIGN="middle">
			<HR NOSHADE />
		</TD>
	</TR>
	
	<TR HEIGHT="30">
		<TD COLSPAN="2" ALIGN="left" VALIGN="middle">
			<ASP:LINKBUTTON RUNAT="server" ID="btn_update" />
			&nbsp;&nbsp;	
			<ASP:LINKBUTTON RUNAT="server" ID="btn_cancel" />
		</TD>        
	</TR>
</TABLE>
