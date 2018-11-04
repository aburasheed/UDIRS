<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Form.Master" AutoEventWireup="true"
    CodeBehind="InvestigationForm.aspx.cs" Inherits="UDIRS.Web.Admin.InvestigationForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
    <script type="text/javascript" src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script type="text/javascript" src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <%--    
<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    --%>
    <script type="text/javascript">


     
        $( document ).ready( function() {
            $( '.spReadMore' ).click( function() {
                if ( $( this ).text() == ' read more>>' ) {
                    $( this ).text( ' read less>>' );
                    $( this ).prev( '.dvComments-grid' ).css( 'height', 'auto' );
                }
                else {
                    $( this ).text( ' read more>>' );
                    $( this ).prev( '.dvComments-grid' ).css( 'height', '16px' );
                }
            } );
            $( '.dvComments-grid' ).each( function() {
                if ( $( this ).text().length > 120 ) {
                    $( this ).next( '.spReadMore' ).css( 'display', 'block' );
                }
                else {
                    $( this ).next( '.spReadMore' ).css( 'display', 'none' );
                }
            } );
        } );



        $( document ).ready( function() {

            $( function() {
                $( "#MainContent_chkInvestigationOthers_0" ).click( function() {
                    if ( $( this ).is( ":checked" ) ) {
                        $( "#MainContent_txtOthers" ).removeAttr( "disabled" );
                        $( "#MainContent_txtOthers" ).focus();
                    } else {
                        $( "#MainContent_txtOthers" ).attr( "disabled", "disabled" );
                    }
                } );
            } );
            $( function() {
                $( ".dateTxt" ).datepicker();
            } );
        } );

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
    <%--    ----------------------------------------------------------------  Reporter Information   ---------------------------------------------------------------- --%>
    <div id="RI" title="ReporterInfo" class="container">
        <div title="ReporterInfo" class="header">
            Reporter Information
        </div>
        <div class="divTableBody">
            <div class="divTableRow">
                <div style="width: 600px; float: left; text-align: left; padding: 8px 10px; margin: 15px 0px 15px 0px;">
                    <b>Name:</b>
                    <asp:Label ID="lblName" Style="padding-left: 5px; font-family: Lucida Bright, Georgia, serif;
                        font-size: 18px; font-style: italic; font-variant: normal;" runat="server" Text="" />
                </div>
            </div>
        </div>
    </div>
    <div class="divTableBody">
        <div class="divTableRow">
            <div style="width: 600px; float: left; text-align: left; padding: 8px 10px; margin: 15px 0px 15px 0px;">
                <b>Contact Number:</b>
                <asp:Label ID="lblContact" Style="padding-left: 5px; font-family: Lucida Bright, Georgia, serif;
                    font-size: 18px; font-style: italic; font-variant: normal;" runat="server" Text="" />
            </div>
        </div>
    </div>
    <div class="divTableBody">
        <div class="divTableRow">
            <div style="width: 600px; float: left; text-align: left; padding: 8px 10px; margin: 15px 0px 15px 0px;">
                <b>Email:</b>
                <asp:Label ID="lblEmail" Style="padding-left: 5px; font-family: Lucida Bright, Georgia, serif;
                    font-size: 18px; font-style: italic; font-variant: normal;" runat="server" Text="" />
            </div>
        </div>
    </div>
    <%-- 
    <div id="PIC-L" title="PIC-Left" class="control-Rightcontainercheckbox controls-container">
        <span style="width: 320px; float: left; text-align: left; padding: 8px 10px; margin: 15px 0px 15px 0px;
            border: 0.4pt solid #CCCC99; border-style: double;">Name: </span>
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
    </div>

    --%>
    <%--    ----------------------------------------------------------------  Incident Details   ---------------------------------------------------------------- --%>
    <div class="divTable">
        <!-- Row-1 -->
        <div class="divTableBody">
            <div class="divTableRow">
                <div style="width: 320px; float: left; text-align: left; padding: 8px 10px; margin: 15px 0px 15px 0px;
                    border: 0.4pt solid #CCCC99; border-style: double;">
                    <b>Refrence Number:</b>
                    <asp:Label ID="lblReference" Style="padding-left: 5px; font-family: Lucida Bright, Georgia, serif;
                        font-size: 18px; font-style: italic; font-variant: normal;" runat="server" Text="" />
                </div>
            </div>
        </div>
        <div class="divTableBody">
            <div class="divTableRow">
                <div class="divTableColum">
                    <b>Date/Time:</b></div>
                <div class="divTableColum" style="width: 280px;">
                    <asp:Label ID="lblDateTime" Style="padding-left: 5px; font-family: Lucida Bright, Georgia, serif;
                        font-size: 18px; font-style: italic; font-variant: normal;" runat="server" />
                </div>
                <div style="width: 100px; float: left; text-align: left; padding: 15px 10px; margin-bottom: 15px;
                    border: 0.4pt solid #999999; border-style: dotted;">
                    <b>Location:</b></div>
                <div class="divTableColum" style="width: 558px;">
                    <asp:Label ID="lblLocation" Style="padding-left: 5px; font-family: Lucida Bright, Georgia, serif;
                        font-size: 18px; font-style: italic; font-variant: normal;" runat="server" Text="" /></div>
            </div>
        </div>
        <!-- Row-2 -->
        <div class="divTableBody">
            <div class="divTableRow">
                <div class="divTableColum">
                    <b>Nature of Incident:</b></div>
                <div class="divTable2ColumData">
                    <asp:Label ID="lblNature" Style="padding-left: 5px; font-family: Lucida Bright, Georgia, serif;
                        font-size: 18px; font-style: italic; font-variant: normal;" runat="server" Text="" /></div>
            </div>
        </div>
        <!-- Row-3 -->
        <div class="divTableBody">
            <div class="divTableRow">
                <div class="divTableColum">
                    <b>Incident Description:</b></div>
                <div class="divTable2ColumData">
                    <asp:Label ID="lblIncidentDesc" Style="padding-left: 5px; font-family: Lucida Bright, Georgia, serif;
                        font-size: 18px; font-style: italic; font-variant: normal;" runat="server" Text="" /></div>
            </div>
        </div>
        <!-- Row-4 -->
        <div class="divTableBody">
            <div class="divTableRow">
                <div class="divTableColum">
                    <b>IsPerson injured:</b><br />
                </div>
                <div class="divTable2ColumData">
                    <asp:Label ID="lblPersonInjured" Style="padding-left: 5px; font-family: Lucida Bright, Georgia, serif;
                        font-size: 18px; font-style: italic; font-variant: normal;" runat="server" Text="" /></div>
            </div>
        </div>
        <!-- Row-5 -->
        <div class="divTableBody">
            <div class="divTableRow">
                <div class="divTableColum">
                    <b>Description of the Injury:</b>
                </div>
                <div class="divTable2ColumData">
                    <asp:Label ID="lblInjuryDesc" Style="padding-left: 5px; font-family: Lucida Bright, Georgia, serif;
                        font-size: 18px; font-style: italic; font-variant: normal;" runat="server" Text="" /></div>
            </div>
        </div>
        <!-- Row-6 -->
        <div class="divTableBody">
            <div class="divTableRow">
                <div class="divTableColum">
                    <b>Treatment Provided:</b></div>
                <div class="divTableColum" style="width: 280px;">
                    <asp:Label ID="lblTreatmentProvided" Style="padding-left: 5px; font-family: Lucida Bright, Georgia, serif;
                        font-size: 18px; font-style: italic; font-variant: normal;" runat="server" Text="" /></div>
                <div style="width: 170px; float: left; text-align: left; padding: 15px 10px; margin-bottom: 15px;
                    border: 0.4pt solid #999999; border-style: dotted;">
                    <b>Type of Treatment:</b></div>
                <div class="divTableColum" style="width: 488px;">
                    <asp:Label ID="lblTypeTeatment" Style="padding-left: 5px; font-family: Lucida Bright, Georgia, serif;
                        font-size: 18px; font-style: italic; font-variant: normal;" runat="server" Text="" /></div>
            </div>
        </div>
        <!-- Row-7 -->
        <div class="divTableBody">
            <div class="divTableRow">
                <div class="divTableColum">
                    <b>Suggested Corrective Action:</b></div>
                <div class="divTable2ColumData">
                    <asp:Label ID="lblSuggestion" Style="padding-left: 5px; font-family: Lucida Bright, Georgia, serif;
                        font-size: 18px; font-style: italic; font-variant: normal;" runat="server" Text="" /></div>
            </div>
        </div>
        <!-- Row-8 -->
        <div class="divTableBody">
            <div class="divTableRow">
                <div class="divTableColum">
                    <b>Corrective Action:</b></div>
                <div class="divTable2ColumData">
                    <asp:Label ID="lblAction" Style="padding-left: 5px; font-family: Lucida Bright, Georgia, serif;
                        font-size: 18px; font-style: italic; font-variant: normal;" runat="server" Text="" />
                </div>
            </div>
        </div>
        <!-- Row-9 -->
        <div class="divTableBody">
            <div class="divTableRow">
                <div class="divTableColum">
                    <b>Additional Information:</b></div>
                <div class="divTable2ColumData">
                    <asp:Label ID="lblAddtional" Style="padding-left: 5px; font-family: Lucida Bright, Georgia, serif;
                        font-size: 18px; font-style: italic; font-variant: normal;" runat="server" Text="" /></div>
            </div>
        </div>
    </div>
    <%--    ----------------------------------------------------------------  Incident Investigation   ---------------------------------------------------------------- --%>
    <div id="IId" title="IncidentInvestigation" class="container">
        <div title="DetailsHeader" class="header">
            Incident Investigation
        </div>
        <div id="TI" title="TypeofIncident" class="SideHeader">
            Type of Incident
        </div>
        <div id="dvTypeofIncident" title="TypeofIncident" class="control-Rightcontainercheckbox incident-count">
            <asp:CheckBoxList ID="chkTypeofIncident" RepeatColumns="4" RepeatDirection="vertical"
                CellPadding="5" CellSpacing="5" runat="server">
            </asp:CheckBoxList>
        </div>
        <div id="EL" title="EvaluationofLoss" class="SideHeader">
            Evaluation of Loss
        </div>
        <div id="dvEvaluationofLoss" title="EvaluationofLoss" class="control-Rightcontainercheckbox incident-count">
            <asp:CheckBoxList ID="chkEvaluationofLoss" RepeatColumns="5" RepeatDirection="vertical"
                CellPadding="5" CellSpacing="5" runat="server">
            </asp:CheckBoxList>
        </div>
        <div id="AI" title="ProbabilityofReoccurrence" class="SideHeader">
            Probability of Reoccurrence
        </div>
        <div id="dvProbabilityofReoccurrence" title="ProbabilityofReoccurrence" class="control-Rightcontainercheckbox incident-count">
            <asp:CheckBoxList ID="chkProbabilityofReoccurrence" RepeatColumns="3" RepeatDirection="vertical"
                CellPadding="5" CellSpacing="5" runat="server">
            </asp:CheckBoxList>
        </div>
    </div>
    <%--    ----------------------------------------------------------------  INCIDENT CAUSE  ---------------------------------------------------------------- --%>
    <div id="ID" title="IncidentInvestigation" class="container">
        <div title="DetailsHeader" class="header">
            Incident Cause
        </div>
        <div title="WhatFactors" style="margin-top: 5px; margin-bottom: 5px; text-align: left;
            margin-left: 8px;">
            <b>What factors contributed to the incident? What caused the event? </b>
        </div>
        <div id="EE" title="EnvironmentEquipment " class="SideHeader">
            Environment / Equipment
        </div>
        <div id="dvEnvironmentEquipment" title="EnvironmentEquipment" class="control-Rightcontainercheckbox incident-count">
            <asp:CheckBoxList ID="chkEnvironmentEquipment" RepeatColumns="4" RepeatDirection="vertical"
                CellPadding="5" CellSpacing="5" runat="server">
            </asp:CheckBoxList>
        </div>
        <div id="HW" title="HumanFactorsWorkSystems" class="SideHeader">
            Human Factors / Work systems
        </div>
        <div id="dvHumanFactorsWorkSystems" title="HumanFactorsWorkSystems" class="control-Rightcontainercheckbox incident-count">
            <asp:CheckBoxList ID="chkHumanFactorsWorkSystems" RepeatColumns="3" RepeatDirection="vertical"
                CellPadding="5" CellSpacing="5" runat="server">
            </asp:CheckBoxList>
        </div>
    </div>
    <%--    ----------------------------------------------------------------  Others  ---------------------------------------------------------------- --%>
    <div id="OS" title="Others" class="SideHeader">
        Others
    </div>
    <div id="dvOthers" title="Others" class="control-Rightcontainercheckbox incident-count">
        <asp:CheckBoxList ID="chkInvestigationOthers" runat="server">
        </asp:CheckBoxList>
        <span>&nbsp( If not listed)</span><br />
        <asp:TextBox ID="txtOthers" TextMode="MultiLine" Style="width: 360px; height: 35px;
            text-align: left;" disabled="disabled" runat="server" />
    </div>
    <%--    ----------------------------------------------------------------  ACTION PLAN  ---------------------------------------------------------------- --%>
    <div id="AId" title="IncidentInvestigation" class="container">
        <div title="DetailsHeader" class="header">
            ACTION PLAN
        </div>
        <div title="WhatFactors" style="margin-top: 5px; margin-bottom: 5px; text-align: left;
            margin-left: 8px;">
            <b>What has or should be done to prevent the incident for reoccurrence ?</b>
        </div>
        <div title="dvGrid" style="margin-top: 5px; margin-bottom: 5px; text-align: left;
            margin-left: 18px;">
            <asp:GridView ID="gvActionPlan" runat="server" AutoGenerateColumns="false">
                <Columns>
                    <asp:TemplateField HeaderText="ActionPlanID" HeaderStyle-CssClass="display_none"
                        ItemStyle-CssClass="display_none">
                        <ItemTemplate>
                            <asp:Label ID="lblID" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%-- ###########################  Action  ################################## --%>
                    <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                            <asp:TextBox ID="txtAction" runat="server"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%-- ###########################  Deadline  ################################## --%>
                    <asp:TemplateField HeaderText="Deadline">
                        <ItemTemplate>
                            <%--<asp:TextBox ID="txtDate" Height="22px" CssClass="controls dateTxt" runat="server" />--%>
                            <asp:TextBox ID="txtDeadline" class="dateTxt" MaxLength="10" runat="server"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="revDeadline" runat="server" ControlToValidate="txtDeadline"
                                Display="Dynamic" ForeColor="#E50000" Font-Size="X-Small" ValidationExpression="^(((0[1-9]|1[012])/(0[1-9]|1\d|2[0-8])|(0[13456789]|1[012])/(29|30)|(0[13578]|1[02])/31)/[2-9]\d{3}|02/29/(([2-9]\d)(0[48]|[2468][048]|[13579][26])|(([2468][048]|[3579][26])00)))$"
                                ErrorMessage="Invalid Date Format"></asp:RegularExpressionValidator>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%-- ###########################  DropDownlist  ################################## --%>
                    <asp:TemplateField HeaderText="IsCompleted">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlCompleted" runat="server" AppendDataBoundItems="true">
                                <asp:ListItem Value="-1">Select</asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%-- ###########################  Remove  ################################## --%>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="lbRemove" OnClick="lbRemove_Click" OnClientClick="return confirm('Are you sure you want to remove this record, Removing will delete it from the database.')"
                                runat="server">Remove</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:Button ID="btnAddNew" runat="server" Style="margin-top: 10px;" Text="Add New Row" />
        </div>
    </div>
    <%--    ----------------------------------------------------------------  Status and Comments ---------------------------------------------------------------- --%>
    <div id="IID" title="IncidentInvestigation" class="container">
        <div title="Status" class="header">
            STATUS
        </div>
        <div title="dvStatus" style="margin-top: 5px; margin-bottom: 5px; text-align: left;
            margin-left: 18px;">
            <b>
              <%--  <asp:DropDownList ID="ddlStatus" CssClass="controls" runat="server">
                    <asp:ListItem Text="Please Select" Value="0" />
                    <asp:ListItem Text="On-Progress" Value="1" />
                    <asp:ListItem Text="Escalated to VDQ" Value="2" />
                    <asp:ListItem Text="Close With Action" Value="3" />
                    <asp:ListItem Text="Close Without Action" Value="4" />
                </asp:DropDownList>--%>

                <asp:DropDownList ID="ddlStatus" runat="server">
                </asp:DropDownList>
            </b>
        </div>
        <div title="dvComments" style="margin-top: 50px; margin-bottom: 5px; text-align: left;
            margin-left: 18px;">
            (<span id="spComments">maximum 500 characters</span>)<br />
            <asp:TextBox ID="txtComments" TextMode="MultiLine" TAMaxLength="500" Style="width: 575px;
                height: 145px; text-align: left;" runat="server" />
        </div>
        <div title="dvCommentsHistory" style="margin-top: 50px; margin-bottom: 5px; text-align: left;
            margin-left: 18px;">
            <asp:GridView ID="gvCommentsHistory" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                EmptyDataText="No Status History Found" GridLines="both" CssClass="RMO-status-history"
                EmptyDataRowStyle-ForeColor="Red">
                <Columns>
                    <asp:BoundField DataField="IncidentID" HeaderText="IncidentID" Visible="false" />
                    <asp:BoundField DataField="Fullname" HeaderText="N A M E" />
                    <asp:BoundField DataField="Dated" HeaderText="Date" ItemStyle-Width="200px" />
                    <asp:TemplateField HeaderText="Comments">
                        <ItemTemplate>
                            <div id="dvComments" class="dvComments-grid" runat="server">
                            </div>
                            <span class="spReadMore">&nbsp;read more&gt;&gt;</span>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="HistoryStatus" HeaderText="Historystatus" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
    <div id="AdI-R3" title="AdI-Right" style="width: 100%; height: 45px; color: #000000;
        text-align: left;">
        <asp:Button ID="btnSave" Text="Save" class="control-Btncontainer incident-count-btn"
            ValidationGroup="Form" runat="server" />
    </div>
  
</asp:Content>
