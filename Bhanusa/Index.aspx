<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Bhanusa.Bhanusa" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.11.1.min.js"></script>
    <script type="text/javascript" src="http://code.jquery.com/ui/1.10.2/jquery-ui.js"></script>
    <script type="text/javascript" src="//code.jquery.com/jquery-1.10.2.js"></script>
    <title>Bhanusaa</title>

    <!-- Google Fonts -->
    <link href='https://fonts.googleapis.com/css?family=Roboto+Slab:400,100,300,700|Lato:400,100,300,700,900' rel='stylesheet' type='text/css'>

    <link rel="stylesheet" href="css/animate.css">
    <!-- Custom Stylesheet -->
    <link rel="stylesheet" href="css/style.css">

    <script type="text/javascript" src="http://ajax.aspnetcdn.com/ajax/jquery/jquery-1.4.4.min.js"></script>
    <script type="text/javascript" src="http://ajax.aspnetcdn.com/ajax/jquery.validate/1.7/jquery.validate.min.js"></script>

    <!-- Login check -->
    <script type="text/javascript">

        (function ($, W, D) {
            var JQUERY4U = {};

            JQUERY4U.UTIL =
            {
                setupFormValidation: function () {
                    //form validation rules
                    $("#login").validate({
                        rules: {
                            empId: {
                                required: true,
                                minlength: 4
                            },
                            empPass: {
                                required: true,
                                minlength: 4
                            },
                        },
                        messages: {
                            empId: "Please enter your Employee Id",
                            empPass: {
                                required: "Please provide a password",
                                minlength: "Password must be atleast 4 characters"
                            },
                        },

                        submitHandler: function () {
                            submit();
                        }

                    });
                }
            }
            //when the dom has loaded setup form validation rules
            $(D).ready(function ($) {
                JQUERY4U.UTIL.setupFormValidation();
            });

        })(jQuery, window, document);

    </script>

    <!--Check Employee -->
    <script type="text/javascript">
        function userdetails() {
            var chkEmpId = $('#txtEmpId').val();
            if (chkEmpId.length == 4) {
                var jsonCheckUser = chkEmpId;
                try {
                    $.ajax({
                        type: "POST",
                        url: "chkUser.ashx",
                        cache: false,
                        data: jsonCheckUser,
                        dataType: "json",
                        success: userChecked,
                        error: function getFail(cpnmsg) {
                            alert(cpnmsg.Response);
                        }
                    });
                } catch (e) {
                    alert(e);
                }
                function userChecked(usrchk) {
                    var userDetailsres = usrchk.Response;
                    if (userDetailsres != "") {
                        var udressplit = userDetailsres.split(',');
                        $('#txtEmpId').hide();
                        $('#lblEmpName').text(udressplit[0]);
                        $('#lblEmpName').show();
                        $('#lblEmpId').text("Employee Name");
                    }
                }
            }
        }
    </script>

    <!--Login Employee -->
    <script type="text/javascript">
        function submit() {
            var pass = $('#txtEmpPass').val();
            if (pass != "") {
                var empId = $('#txtEmpId').val();
                var jsonCheckUser = empId + "," + pass;
                try {
                    $.ajax({
                        type: "POST",
                        url: "chkUser.ashx",
                        cache: false,
                        data: jsonCheckUser,
                        dataType: "json",
                        success: userValid,
                        error: function getFail(cpnmsg) {
                            alert("Incorrect Password!!");
                        }
                    });
                } catch (e) {
                    alert(e);
                }
                function userValid(usrvld) {
                    var userLoginPage = usrvld.Response;
                    if (userLoginPage != "") {
                        var uddetsplit = userLoginPage.split(',');
                        if (userLoginPage != "") {
                            var url = "SuperAdmin.aspx?EmpName=" + encodeURIComponent($("#lblEmpName").text());
                            window.location.href = url;
                        }
                    }
                }
            }
        }
    </script>
</head>
<body>
    <div class="container">
        <div class="top">
            <h1 id="title" class="hidden"><span id="logo">Bhanusaa</span></h1>
        </div>
        <form id="login" runat="server" method="post" autocomplete="on">
            <div class="login-box animated fadeInUp">
                <div class="box-header">
                    <h2>Log In</h2>
                </div>
                <label id="lblEmpId" for="txtEmpId">Employee ID</label>
                <br />
                <input type="text" id="txtEmpId" autocomplete="off" name="empId" onchange="userdetails();" required="required" />
                <label id="lblEmpName" visible="false" for="txtEmpId"></label>
                <br />
                <label for="txtEmpPass">Password</label>
                <br />
                <input type="password" id="txtEmpPass" autocomplete="off" name="empPass" required="required" />
                <br />
                <input type="submit" id="btnSubmit" value="Sign In" />
                <br />
                <a href="#">
                    <p class="small">Forgot your password?</p>
                </a>
            </div>
        </form>
    </div>
</body>
</html>
