﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Form.Master" AutoEventWireup="true"
    MaintainScrollPositionOnPostback="true" CodeBehind="Form.aspx.cs" Inherits="UDIRS.Web.Form" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
     <link rel="stylesheet" href="/resources/demos/style.css" />
    <script type="text/javascript" src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script type="text/javascript" src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
<%--    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
--%>    <script type="text/javascript">
        $( document ).ready( function() {
            // Only two "Nature of Incident" can be selected
            var CountIncidents = 0;
            $( '.incident-count input[type = "checkbox"]' ).click( function() {
                if ( $( this ).is( ':checked' ) )
                    CountIncidents = CountIncidents + 1;
                else
                    CountIncidents = CountIncidents - 1;
            } );
            $( '.incident-count-btn' ).click( function() {
                if ( CountIncidents == 0 )
                { alert( 'Select atleast one "Nature of Incident"' ); return false; }
                else if ( CountIncidents > 0 && CountIncidents <= 2 ) { return true; }
                alert( 'Only two "Nature of Incident" can be selected' )
                return false;
            } );

            //      were you or another person injured?
            $( ".injury-desc" ).attr( "disabled", true );
            $( '.rblInjured input' ).click( function() {
                if ( $( this ).val() == "N" ) {
                    $( ".injury-desc" ).attr( "disabled", true );
                }
                else {
                    $( ".injury-desc" ).attr( "disabled", false );

                }

            } );

            //      Was medical treatment provided
            $( ".treatment-place input" ).attr( "disabled", true );
            $( '.treatment-provided input' ).click( function() {
                if ( $( this ).val() == "N" ) {
                    $( ".treatment-place input" ).attr( "disabled", true );
                }
                else {
                    $( ".treatment-place input" ).attr( "disabled", false );
                }
            } );
            $( function() {
                $( ".dateTxt" ).datepicker();
            } );
        } );


        //      otherIncidents

        $( function() {
            $( "#MainContent_chkOthers_0" ).click( function() {
                if ( $( this ).is( ":checked" ) ) {
                    $( "#MainContent_txtOthers" ).removeAttr( "disabled" );
                    $( "#MainContent_txtOthers" ).focus();
                } else {
                    $( "#MainContent_txtOthers" ).attr( "disabled", "disabled" );
                }
            } );
        } );



        //      other Locations
        function showHideOther() {
            if ( $( "#MainContent_ddlLocation option:selected" ).text() == "Other" ) {
                $( "#MainContent_txtOtherlocation" ).css( 'display', 'block' );
                //alert( "I am Other" );
            }
            else {
                $( "#MainContent_txtOtherlocation" ).css( 'display', 'none' );
                //alert( "I am not Other" );
            }
        }



        // --------------- jump to top position -----------------------------
        window.scrollTo = function( x, y ) {
            return true;
        }


        // --------------- Max 500 Characters-----------------------------
        function checkMaxLength( e, pThis, spanID ) {
            var nMaxLen = pThis.getAttribute( "TAMaxLength" );
            limitTextarea( pThis, spanID );
            switch ( e.keyCode ) {
                case 37: // left
                    return true;
                case 38: // up
                    return true;
                case 39: // right
                    return true;
                case 40: // down
                    return true;
                case 8: // backspace
                    return true;
                case 46: // delete
                    return true;
                case 27: // escape
                    //pThis.value='';
                    return true;
            }
            if ( pThis.value.length > nMaxLen ) {
                pThis.value = pThis.value.substr( 0, nMaxLen );
            }
            return ( pThis.value.length < nMaxLen );
        }

        function limitTextarea( pThis, spanID ) {
            //var oComments = document.getElementById(pThis.id);
            var oSpanRemaining = document.getElementById( spanID );
            var nMaxLen = pThis.getAttribute( "TAMaxLength" );
            var nCharsRemaining;
            nCharsRemaining = nMaxLen - pThis.value.length;
            if ( nCharsRemaining < 0 ) {
                nCharsRemaining = 0;
            }
            oSpanRemaining.innerHTML = nCharsRemaining + " remaining";
        }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="PI" title="PersonalInfo" class="container">
        <div title="PersonalInfo" class="header">
            Personal Information
        </div>
        <div id="PIC-L" title="PIC-Left" class="control-Rightcontainercheckbox controls-container">
            <span>Name: </span>
            <asp:Label ID="lblName" Style="float: left; margin-top: -24px; margin-left: 200px;
                border-width: 1px; border-style: groove; border-color: #3e753f4f; height: 27px;
                width: 250px;" runat="server"></asp:Label>
            <span>Contact Number: </span>
            <asp:Label ID="lblContact" Style="float: left; margin-top: -24px; margin-left: 200px;
                border-width: 1px; border-style: groove; border-color: #3e753f4f; height: 27px;
                width: 250px;" runat="server"></asp:Label>
            <span>Email: </span>
            <asp:Label ID="lblEmail" Style="float: left; margin-top: -24px; margin-left: 200px;
                border-width: 1px; border-style: groove; border-color: #3e753f4f; height: 27px;
                width: 250px;" runat="server"></asp:Label>
            <%-- 
              <asp:TextBox ID="txtName" Height="22px" Text="" CssClass="txt-lbl" runat="server" />
              <asp:TextBox ID="txtEmail" Height="22px" Text="" CssClass="txt-lbl" runat="server" /> 
              <asp:TextBox ID="txtContact" Height="22px" Text="" .lbl-txt runat="server" />
              <span>Gender: </span>
            <asp:Label ID="lblGender" runat="server" ></asp:Label> <%-- <asp:TextBox ID="txtGender" Height="22px" Text="" CssClass="txt-lbl" runat="server" /> 
            <span>National / Iqama ID: </span>
            <asp:Label ID="lblIqama" CssClass="lbl-txt" runat="server" ></asp:Label> <asp:TextBox ID="txtIqama" Height="22px" Text="" CssClass="txt-lbl" runat="server" /> 
            --%>
        </div>
    </div>
    <%--    ----------------------------------------------------------------  Incident Information   ---------------------------------------------------------------- --%>
    <div id="II" title="IncidentInfo" class="container">
        <div title="IncidentHeader" class="header">
            Incident Information
        </div>
        <div id="IIC-L" title="IIC-Left" class="control-Rightcontainercheckbox">
        <div class="label">
               Upload Image: <%--<asp:FileUpload ID="fuImage" runat="server" />--%>
            </div>
            <asp:FileUpload ID="fuImage" runat="server" />
            <div class="label">
                Date of the Incident:
                <asp:RequiredFieldValidator ID="rvDate" ErrorMessage="*" Style="color: Red;" ControlToValidate="txtDate"
                    ValidationGroup="Form" SetFocusOnError="true" runat="server" />
            </div>
            <%--<asp:HiddenField ID="HiddenField1" runat="server" />--%>
            <asp:TextBox ID="txtDate" Height="22px" CssClass="controls dateTxt" runat="server" />
            <div class="label">
                Hours&nbsp;/&nbsp;Minutes:
            </div>
            <asp:DropDownList ID="ddlHours" runat="server" CssClass="controls">
            </asp:DropDownList>
            <asp:DropDownList ID="ddlMinutes" runat="server" Style="float: left; margin-top: 10px;
                margin-left: 8px; height: 24px;">
            </asp:DropDownList>
            <div class="label">
                Incident Location:
                <asp:RequiredFieldValidator ID="rvLocation" ErrorMessage="*" Style="color: Red;"
                    ControlToValidate="ddlLocation" ValidationGroup="Form" SetFocusOnError="true"
                    runat="server" />
            </div>
            <asp:DropDownList ID="ddlLocation" Width="260px" runat="server" CssClass="controls"
                onchange="return showHideOther();">
                <%--onchange="alert('I am Changed');">--%>
                <%--onchange="test();">--%>
            </asp:DropDownList>
            <asp:TextBox ID="txtOtherlocation" Style="display: none; margin-top: 85px; margin-left: 465px;"
                runat="server" />
            <div style="float: left; margin-top: 8px;">
                Describe the incident in details(exact location, what happened, factors leading
                to the event, etc)be as specific as possible:
                <asp:RequiredFieldValidator ID="rvIncidentDesc" ErrorMessage="*" Style="color: Red;"
                    ControlToValidate="txtIncidentDesc" ValidationGroup="Form" SetFocusOnError="true"
                    runat="server" /><br />(<span id="spIncidentRemaining">maximum 500 characters</span>)
            </div>
            
            <asp:TextBox ID="txtIncidentDesc" TextMode="MultiLine" TAMaxLength="500" Style="width: 1042px; height: 200px;
                text-align: left; margin-top: 8px;" runat="server" />
        </div>
    </div>
    <%--    ----------------------------------------------------------------  Injury   ---------------------------------------------------------------- --%>
    <div id="IJ" title="Injury" class="container">
        <div title="SeverityHeader" class="header">
            Injury
        </div>
        <div id="IJ-L" title="IIC-Left" class="control-Rightcontainercheckbox">
            <b>Were you or another person injured?</b>
            <asp:RadioButtonList ID="rblInjured" RepeatColumns="2" runat="server" CssClass="rblInjured">
                <asp:ListItem Text="Yes" Value="Y" />
                <asp:ListItem Text="No" Value="N" Selected="True" />
            </asp:RadioButtonList>
            <div style="margin-top: 5px;">
                If Yes, describe the injury(burn, laceration, etc)the part of body injuried and
                any other information you can provide regarding the injury: <br />(<span id="spInjuryRemaining">maximum 500 characters</span>)
                <%--<asp:RequiredFieldValidator ID="rvInjuryDesc" ErrorMessage="*" Style="color: Red;" ControlToValidate="txtInjuryDesc"
                    ValidationGroup="Form" SetFocusOnError="true" runat="server" />--%></div>
                    
            <asp:TextBox ID="txtInjuryDesc" TextMode="MultiLine" TAMaxLength="500" Style="width: 1042px; height: 200px;
                margin-top: 8px; text-align: left;" runat="server" CssClass="injury-desc" />
            <br />
            <b>Was medical treatment provided</b>
            <asp:RadioButtonList ID="rblTreatmentProvided" RepeatColumns="2" runat="server" CssClass="treatment-provided">
                <asp:ListItem Text="Yes" Value="Y" />
                <%--Value="yes"--%>
                <asp:ListItem Text="No" Value="N" Selected="True" />
                <%--Value="no"--%>
            </asp:RadioButtonList>
            <div style="margin-top: 5px;">
                If Yes, where was medical treatment provided?</div>
            <asp:RadioButtonList ID="rblTreatmentPlace" RepeatColumns="3" runat="server" CssClass="treatment-place">
                <%-- <asp:ListItem Text="On site, First Aid" Value=""  />           <%--Value="FirstAid"--%>
                <%-- <asp:ListItem Text="Clinic/Healthcare Center" Value="" />      <%--Value="Clinic"--%>
                <%-- <asp:ListItem Text="Hospital" Value="" />                      <%--Value="Hospital"--%>
            </asp:RadioButtonList>
        </div>
    </div>
    <%--    ----------------------------------------------------------------  Nature of Incident   ---------------------------------------------------------------- --%>
    <div id="ID" title="IncidentDetails" class="container">
        <div title="DetailsHeader" class="header">
            Nature of Incident (Check Max '2' only)
        </div>
        <div id="ID-R" title="ID-Right">
            <div id="NI" title="HealthSafety" class="SideHeader">
                Health and Safety
            </div>
            <div id="dvHealthSafety" title="HealthSafety" class="control-Rightcontainercheckbox incident-count">
                <asp:CheckBoxList ID="chkHealthSafety" RepeatColumns="4" RepeatDirection="vertical"
                    CellPadding="5" CellSpacing="5" runat="server">
                </asp:CheckBoxList>
            </div>
            <div id="IS" title="InformationSystem" class="SideHeader">
                Information System
            </div>
            <div id="dvInformationSystem" title="InformationSystem" class="control-Rightcontainercheckbox incident-count">
                <asp:CheckBoxList ID="chkInformationSystem" RepeatColumns="5" RepeatDirection="vertical"
                    CellPadding="5" CellSpacing="5" runat="server">
                </asp:CheckBoxList>
            </div>
            <div id="AI" title="AcademicIntegrity" class="SideHeader">
                Academic Integrity
            </div>
            <div id="dvAcademicIntegrity" title="AcademicIntegrity" class="control-Rightcontainercheckbox incident-count">
                <asp:CheckBoxList ID="chkAcademicIntegrity" RepeatColumns="2" RepeatDirection="vertical"
                    CellPadding="5" CellSpacing="5" runat="server">
                </asp:CheckBoxList>
            </div>
            <div id="RE" title="RegulationsEthics" class="SideHeader">
                Regulations and Ethics
            </div>
            <div id="dvRegulationsEthics" title="RegulationsEthics" class="control-Rightcontainercheckbox incident-count">
                <asp:CheckBoxList ID="chkRegulationsEthics" RepeatColumns="1" RepeatDirection="vertical"
                    CellPadding="5" CellSpacing="5" runat="server">
                </asp:CheckBoxList>
            </div>
            <div id="OS" title="Others" class="SideHeader">
                Others
            </div>
            <div id="dvOthers" title="Others" class="control-Rightcontainercheckbox incident-count">
                <asp:CheckBoxList ID="chkOthers" runat="server">
                </asp:CheckBoxList>
                <span>&nbsp( If not listed in Nature of Incident )</span><br />
                <asp:TextBox ID="txtOthers" TextMode="MultiLine" Style="width: 360px; height: 35px;
                    text-align: left;" disabled="disabled" runat="server" />
            </div>
            <%--<div id="OS" title="Others" class="SideHeaderOthers">
                <p style="margin-top: 50px; color: #000000; font-family: monospace;">
                    Others
                </p>
            </div>
            <div id="dvOthers" title="Others" style="width: 1200px; color: #000000; text-align: left;
                padding: 15px 0px 0px 15px; height: 120px; float: left;">
                <asp:CheckBox ID="cbOthers" AutoPostBack="true" runat="server" OnCheckedChanged="cbOthers_CheckedChanged" /><b>Others</b>
                <asp:TextBox ID="txtOthers" TextMode="MultiLine" Style="width: 650px; height: 80px;
                    text-align: left; display: none" runat="server" />
            </div>--%>
        </div>
    </div>
    <%--    ----------------------------------------------------------------  Corrective Action   ---------------------------------------------------------------- --%>
    <div id="CA" title="CorrectiveAction" class="container">
        <div title="TypeHeader" class="header">
            Corrective Action
        </div>
        <div id="CA-R" title="CA-Right" class="control-Rightcontainercheckbox">
            <span>Suggested corrective action to be taken to prevent reoccurrence of the incident</span>
            (<span id="spCorrectiveRemaining">maximum 500 characters</span>)
            <asp:CheckBoxList ID="chkCorrectiveAction" RepeatColumns="3" RepeatDirection="vertical"
                CellPadding="5" CellSpacing="5" runat="server">
            </asp:CheckBoxList>
            <asp:TextBox ID="txtCorrectiveActionDesc" TextMode="MultiLine" TAMaxLength="500" Style="width: 1042px;
                height: 150px; text-align: left;" runat="server" />
        </div>
    </div>
    <%--    ----------------------------------------------------------------  Additional Information   ---------------------------------------------------------------- --%>
    <div id="AdI" title="AdditionalInformation" class="container">
        <div title="TypeHeader" class="header">
            Additional Information
        </div>
        <div id="AdI-R" title="AdI-Right" class="control-Rightcontainercheckbox">
            <span>Include anything you think is relevant to the investigation<br />
                (this might include possible cause of the incident, actions taken to prevent reoccurrence/make
                safe?, recommendations, etc)</span>
                (<span id="spAdditionalRemaining">maximum 500 characters</span>)
            <%--<asp:TextBox ID="txtAdditionalInformationDesc" TextMode="MultiLine" TAMaxLength="500"--%>
            <asp:TextBox ID="txtAdditionalInformationDesc" TextMode="MultiLine" TAMaxLength="500" 
                Style="width: 1042px; margin-top: 10px; height: 150px; text-align: left;" runat="server" />
        </div>
        <div id="AdI-R3" title="AdI-Right" style="width: 1200px; color: #000000; text-align: left;">
            <asp:Button ID="btnSave" Text="Save" Style="vertical-align: middle" class="control-Btncontainer incident-count-btn"
                ValidationGroup="Form" runat="server" />
        </div>
    </div>
    <br />
</asp:Content>
