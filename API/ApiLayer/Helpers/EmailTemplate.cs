namespace CREL_BE.Helpers;

public static class EmailTemplate
{
    public const string htmlResetPasswordTemplate = @"
<!DOCTYPE html>
<html>
  <head>
    <style type=""text/css"">
      a .yshortcuts:hover {
        background-color: transparent !important;
        border: none !important;
        color: inherit !important
      }
      a .yshortcuts:active {
        background-color: transparent !important;
        border: none !important;
        color: inherit !important
      }
      a .yshortcuts:focus {
        background-color: transparent !important;
        border: none !important;
        color: inherit !important
      }
    </style>
    <style media=""only screen and (max-width: 520px)"" type=""text/css"">
      /* /\/\/\/\/\/\/\/\/ RESPONSIVE MOJO /\/\/\/\/\/\/\/\/ */
      /*  must escape media query with double symbol */
      @media only screen and (max-width: 520px) {
        .main-table {
          width: 90% !important;
        }
        .top {
          padding-top: 33px !important;
          padding-bottom: 37px !important;
        }
        .content {
          padding: 24px 29px !important;
        }
        .verify-button {
          padding: 25px 0 !important;
        }
      }
    </style>
  </head>
  <body align=""center"" style=""margin:0; padding:0; -webkit-text-size-adjust:100%; -ms-text-size-adjust:100%; background:#ffffff; width:100%; font-family:'Roboto',sans-serif; font-size:16px; text-align:center; line-height:22px; color:#AAB2BA"" width=""100%"">
    <table class=""main-table"" height=""100%"" style=""border-collapse:collapse !important; mso-table-lspace:0pt; mso-table-rspace:0pt; font-family:'Roboto', sans-serif; padding-left:0; padding-right:0; padding-top:0; padding-bottom:0; margin:20px auto 10px; padding:0; height:100%; width:80%; max-width:600px"" width=""80%"">
      <tr>
        <td align=""center"" class=""top"" style=""border-collapse:collapse !important; mso-table-lspace:0pt; mso-table-rspace:0pt; font-family:'Roboto', sans-serif; padding-left:0; padding-right:0; padding-top:72px; padding-bottom:48px"" valign=""top"">
          <a data-click-track-id=""3182"" href=""http://crel.site"" style=""color:#3999c1 !important"" title=""crelSite""><img alt=""Similar Web"" class=""1ex"" height=""auto"" src=""https://i.ibb.co/6vD02sj/crel-full-color-tb.png"" style=""width:140px; line-height:100%; border:0; outline:none; text-decoration:none"" width=""159"" /></a>
        </td>
      </tr>
      <tr>
        <td align=""center"" style=""border-collapse:collapse !important; mso-table-lspace:0pt; mso-table-rspace:0pt; font-family:'Roboto', sans-serif; padding-left:0; padding-right:0; padding-top:0; padding-bottom:0"" valign=""top"">

          <!-- BODY -->
          <div style=""border: 1px solid rgba(223,226,230,0.6); border-radius: 4px; background-image:url(https://d1pgqke3goo8l6.cloudfront.net/DNHYRhpZQ2G5MrcSDPDm_help%20wave%402x.png); background-repeat: no-repeat; background-position: bottom -3px right -3px; background-size: 36%;"">
            <table class=""container"" style=""border-collapse:collapse !important; mso-table-lspace:0pt; mso-table-rspace:0pt; font-family:'Roboto', sans-serif; padding-left:0; padding-right:0; padding-top:0; padding-bottom:0; width:100%; max-width:600px; margin:0 auto; padding:0; clear:both"" width=""100%"">
              <tr>
                <td style=""border-collapse:collapse !important; mso-table-lspace:0pt; mso-table-rspace:0pt; font-family:'Roboto', sans-serif; padding-left:0; padding-right:0; padding-top:0; padding-bottom:0"">
                  <table class=""row"" style=""border-collapse:collapse !important; mso-table-lspace:0pt; mso-table-rspace:0pt; font-family:'Roboto', sans-serif; padding-left:0; padding-right:0; padding-top:0; padding-bottom:0; width:100%"" width=""100%"">
                    <tr>
                      <td class=""content"" style=""border-collapse:collapse !important; mso-table-lspace:0pt; mso-table-rspace:0pt; font-family:'Roboto', sans-serif; padding-top:48px; padding-right:48px; padding-bottom:48px; padding-left:48px"">
                        <table class=""row"" style=""border-collapse:collapse !important; mso-table-lspace:0pt; mso-table-rspace:0pt; font-family:'Roboto', sans-serif; padding-left:0; padding-right:0; padding-top:0; padding-bottom:0; width:100%"" width=""100%"">
                          <tr>
                            <td style=""border-collapse:collapse !important; mso-table-lspace:0pt; mso-table-rspace:0pt; padding-left:0; padding-right:0; padding-top:0; padding-bottom:0; font-family:'Roboto', sans-serif; font-size:24px; line-height:38px; color:#1B2653"">
                              Xin ch??o <strong><CrelName></strong>,
                            </td>
                          </tr>
                          <tr>
                            <td style=""border-collapse:collapse !important; mso-table-lspace:0pt; mso-table-rspace:0pt; font-family:'Roboto', sans-serif; padding-left:0; padding-right:0; color:#2A3E52; padding-top:16px; padding-bottom:0px"">T??i kho???n c???a b???n ???? ???????c ?????i m???t kh???u m???i. 
                              Sau khi ????ng nh???p, xin h??y ?????i m???t kh???u ????? b???o v??? t??i kho???n c???a b???n.</td>
                          </tr>
                          <tr>
                            <td style=""border-collapse:collapse !important; mso-table-lspace:0pt; mso-table-rspace:0pt; font-family:'Roboto', sans-serif; padding-left:0; padding-right:0; color:#2A3E52; padding-top:16px; padding-bottom:0px"">
                              N???u c?? v???n ????? v???i vi???c ????ng nh???p h??y li??n h??? <span style=""color:#4F8DF9 !important; text-decoration:none"" target=""_blank"">crel2022@gmail.com</span>
                            </td>
                          </tr>
                          <tr>
                            <td align=""center"" style=""border-collapse:collapse !important; mso-table-lspace:0pt; mso-table-rspace:0pt; font-family:'Roboto', sans-serif; padding-left:0; padding-right:0; padding-bottom:0; color:#000; font-weight:bold; font-size:24px; padding-top:35px; padding-bottom:35px; text-align:center"">
                              <strong><CrelNewPassword></strong>
                            </td>
                          </tr>
                          <tr>
                            <td style=""border-collapse:collapse !important; mso-table-lspace:0pt; mso-table-rspace:0pt; color:#2A3E52; font-family:'Roboto', sans-serif; font-size:16px; line-height:22px; padding-top:0px; padding-right:0px; padding-bottom:26px; padding-left:0"">
                              Ch??c b???n c?? m???t ng??y tuy???t v???i, <br />CREL
                            </td>
                          </tr>
                          <tr>
                            <td style=""border-collapse:collapse !important; mso-table-lspace:0pt; mso-table-rspace:0pt; padding-left:0; padding-top:0; padding-bottom:0; font-family:'Roboto', sans-serif; font-size:14px; line-height:16px; padding-right:80px"">
                              C???n gi??p ?????? H??y g???i mail t???i <span style=""color:#4F8DF9 !important; text-decoration:none"" target=""_blank"">crel2022@gmail.com</span>
                            </td>
                          </tr>
                        </table>
                      </td>
                    </tr>
                  </table>
                </td>
              </tr>
            </table>
          </div>

          <!-- BODY END -->
        </td>
      </tr>
      
      
    </table>
  </body>
</html>
";

    public const string htmlAccountCreateTemplate = @"
<!DOCTYPE html>
<html>
  <head>
    <style type=""text/css"">
      a .yshortcuts:hover {
        background-color: transparent !important;
        border: none !important;
        color: inherit !important;
      }
      a .yshortcuts:active {
        background-color: transparent !important;
        border: none !important;
        color: inherit !important;
      }
      a .yshortcuts:focus {
        background-color: transparent !important;
        border: none !important;
        color: inherit !important;
      }
    </style>
    <style media=""only screen and (max-width: 520px)"" type=""text/css"">
      /* /\/\/\/\/\/\/\/\/ RESPONSIVE MOJO /\/\/\/\/\/\/\/\/ */
      /*  must escape media query with double symbol */
      @media only screen and (max-width: 520px) {
        .main-table {
          width: 90% !important;
        }
        .top {
          padding-top: 33px !important;
          padding-bottom: 37px !important;
        }
        .content {
          padding: 24px 29px !important;
        }
        .verify-button {
          padding: 25px 0 !important;
        }
      }
    </style>
  </head>
  <body
    align=""center""
    style=""
      margin: 0;
      padding: 0;
      -webkit-text-size-adjust: 100%;
      -ms-text-size-adjust: 100%;
      background: #ffffff;
      width: 100%;
      font-family: 'Roboto', sans-serif;
      font-size: 16px;
      text-align: center;
      line-height: 22px;
      color: #aab2ba;
    ""
    width=""100%""
  >
    <table
      class=""main-table""
      height=""100%""
      style=""
        border-collapse: collapse !important;
        mso-table-lspace: 0pt;
        mso-table-rspace: 0pt;
        font-family: 'Roboto', sans-serif;
        padding-left: 0;
        padding-right: 0;
        padding-top: 0;
        padding-bottom: 0;
        margin: 20px auto 10px;
        padding: 0;
        height: 100%;
        width: 80%;
        max-width: 600px;
      ""
      width=""80%""
    >
      <tr>
        <td
          align=""center""
          class=""top""
          style=""
            border-collapse: collapse !important;
            mso-table-lspace: 0pt;
            mso-table-rspace: 0pt;
            font-family: 'Roboto', sans-serif;
            padding-left: 0;
            padding-right: 0;
            padding-top: 72px;
            padding-bottom: 48px;
          ""
          valign=""top""
        >
          <a
            data-click-track-id=""3182""
            href=""http://crel.site""
            style=""color: #3999c1 !important""
            title=""crelSite""
            ><img
              alt=""Similar Web""
              class=""1ex""
              height=""auto""
              src=""https://i.ibb.co/6vD02sj/crel-full-color-tb.png""
              style=""
                width: 140px;
                line-height: 100%;
                border: 0;
                outline: none;
                text-decoration: none;
              ""
              width=""159""
          /></a>
        </td>
      </tr>
      <tr>
        <td
          align=""center""
          style=""
            border-collapse: collapse !important;
            mso-table-lspace: 0pt;
            mso-table-rspace: 0pt;
            font-family: 'Roboto', sans-serif;
            padding-left: 0;
            padding-right: 0;
            padding-top: 0;
            padding-bottom: 0;
          ""
          valign=""top""
        >
          <!-- BODY -->
          <div
            style=""
              border: 1px solid rgba(223, 226, 230, 0.6);
              border-radius: 4px;
              background-image: url(https://i.ibb.co/W3FbLGf/8244.jpg);
              background-repeat: no-repeat;
              background-position: bottom -3px right -3px;
              background-size: 36%;
            ""
          >
            <table
              class=""container""
              style=""
                border-collapse: collapse !important;
                mso-table-lspace: 0pt;
                mso-table-rspace: 0pt;
                font-family: 'Roboto', sans-serif;
                padding-left: 0;
                padding-right: 0;
                padding-top: 0;
                padding-bottom: 0;
                width: 100%;
                max-width: 600px;
                margin: 0 auto;
                padding: 0;
                clear: both;
              ""
              width=""100%""
            >
              <tr>
                <td
                  style=""
                    border-collapse: collapse !important;
                    mso-table-lspace: 0pt;
                    mso-table-rspace: 0pt;
                    font-family: 'Roboto', sans-serif;
                    padding-left: 0;
                    padding-right: 0;
                    padding-top: 0;
                    padding-bottom: 0;
                  ""
                >
                  <table
                    class=""row""
                    style=""
                      border-collapse: collapse !important;
                      mso-table-lspace: 0pt;
                      mso-table-rspace: 0pt;
                      font-family: 'Roboto', sans-serif;
                      padding-left: 0;
                      padding-right: 0;
                      padding-top: 0;
                      padding-bottom: 0;
                      width: 100%;
                    ""
                    width=""100%""
                  >
                    <tr>
                      <td
                        class=""content""
                        style=""
                          border-collapse: collapse !important;
                          mso-table-lspace: 0pt;
                          mso-table-rspace: 0pt;
                          font-family: 'Roboto', sans-serif;
                          padding-top: 48px;
                          padding-right: 48px;
                          padding-bottom: 48px;
                          padding-left: 48px;
                        ""
                      >
                        <table
                          class=""row""
                          style=""
                            border-collapse: collapse !important;
                            mso-table-lspace: 0pt;
                            mso-table-rspace: 0pt;
                            font-family: 'Roboto', sans-serif;
                            padding-left: 0;
                            padding-right: 0;
                            padding-top: 0;
                            padding-bottom: 0;
                            width: 100%;
                          ""
                          width=""100%""
                        >
                          <tr>
                            <td
                              style=""
                                border-collapse: collapse !important;
                                mso-table-lspace: 0pt;
                                mso-table-rspace: 0pt;
                                padding-left: 0;
                                padding-right: 0;
                                padding-top: 0;
                                padding-bottom: 0;
                                font-family: 'Roboto', sans-serif;
                                font-size: 24px;
                                line-height: 38px;
                                color: #1b2653;
                              ""
                            >
                              Ch??o m???ng <CrelName> ???? ?????n v???i CREL!
                            </td>
                          </tr>
                          <tr>
                            <td
                              style=""
                                border-collapse: collapse !important;
                                mso-table-lspace: 0pt;
                                mso-table-rspace: 0pt;
                                font-family: 'Roboto', sans-serif;
                                padding-left: 0;
                                padding-right: 0;
                                color: #2a3e52;
                                padding-top: 16px;
                                padding-bottom: 0px;
                              ""
                            >
                              <p>B??y gi??? b???n ???? c?? th??? ????ng nh???p v??o h??? th???ng CREL v???i <br/>T??i kho???n: <strong><CrelUserName></strong> <br/> M???t kh???u: <strong><CrelPassword></strong></p>
                          </tr>
                          
                          <tr>
                            <td
                              align=""center""
                              style=""
                                border-collapse: collapse !important;
                                mso-table-lspace: 0pt;
                                mso-table-rspace: 0pt;
                                font-family: 'Roboto', sans-serif;
                                padding-left: 0;
                                padding-right: 0;
                                padding-bottom: 0;
                                color: #000;
                                font-weight: bold;
                                font-size: 24px;
                                padding-top: 35px;
                                padding-bottom: 35px;
                                text-align: center;
                              ""
                            >
                              
                            </td>
                          </tr>
                          <tr>
                            <td
                              style=""
                                border-collapse: collapse !important;
                                mso-table-lspace: 0pt;
                                mso-table-rspace: 0pt;
                                color: #2a3e52;
                                font-family: 'Roboto', sans-serif;
                                font-size: 16px;
                                line-height: 22px;
                                padding-top: 0px;
                                padding-right: 0px;
                                padding-bottom: 26px;
                                padding-left: 0;
                              ""
                            >
                              Ch??c b???n c?? m???t ng??y tuy???t v???i, <br />CREL
                            </td>
                          </tr>
                          <tr>
                            <td
                              style=""
                                border-collapse: collapse !important;
                                mso-table-lspace: 0pt;
                                mso-table-rspace: 0pt;
                                padding-left: 0;
                                padding-top: 0;
                                padding-bottom: 0;
                                font-family: 'Roboto', sans-serif;
                                font-size: 14px;
                                line-height: 16px;
                                padding-right: 80px;
                              ""
                            >
                              C???n gi??p ?????? H??y g???i mail t???i
                              <span
                                style=""
                                  color: #4f8df9 !important;
                                  text-decoration: none;
                                ""
                                target=""_blank""
                                >crel2022@gmail.com</span
                              >
                            </td>
                          </tr>
                        </table>
                      </td>
                    </tr>
                  </table>
                </td>
              </tr>
            </table>
          </div>

          <!-- BODY END -->
        </td>
      </tr>
    </table>
  </body>
</html>

";

    public const string htmlAreaManagerSuggestTemplate = @"
<!DOCTYPE html>
<html>
  <head>
    <style type=""text/css"">
      a .yshortcuts:hover {
        background-color: transparent !important;
        border: none !important;
        color: inherit !important;
      }
      a .yshortcuts:active {
        background-color: transparent !important;
        border: none !important;
        color: inherit !important;
      }
      a .yshortcuts:focus {
        background-color: transparent !important;
        border: none !important;
        color: inherit !important;
      }
    </style>
    <style media=""only screen and (max-width: 520px)"" type=""text/css"">
      /* /\/\/\/\/\/\/\/\/ RESPONSIVE MOJO /\/\/\/\/\/\/\/\/ */
      /*  must escape media query with double symbol */
      @media only screen and (max-width: 520px) {
        .main-table {
          width: 90% !important;
        }
        .top {
          padding-top: 33px !important;
          padding-bottom: 37px !important;
        }
        .content {
          padding: 24px 29px !important;
        }
        .verify-button {
          padding: 25px 0 !important;
        }
      }
    </style>
  </head>
  <body
    align=""center""
    style=""
      margin: 0;
      padding: 0;
      -webkit-text-size-adjust: 100%;
      -ms-text-size-adjust: 100%;
      background: #ffffff;
      width: 100%;
      font-family: 'Roboto', sans-serif;
      font-size: 16px;
      text-align: center;
      line-height: 22px;
      color: #aab2ba;
    ""
    width=""100%""
  >
    <table
      class=""main-table""
      height=""100%""
      style=""
        border-collapse: collapse !important;
        mso-table-lspace: 0pt;
        mso-table-rspace: 0pt;
        font-family: 'Roboto', sans-serif;
        padding-left: 0;
        padding-right: 0;
        padding-top: 0;
        padding-bottom: 0;
        margin: 20px auto 10px;
        padding: 0;
        height: 100%;
        width: 80%;
        max-width: 600px;
      ""
      width=""80%""
    >
      <tr>
        <td
          align=""center""
          class=""top""
          style=""
            border-collapse: collapse !important;
            mso-table-lspace: 0pt;
            mso-table-rspace: 0pt;
            font-family: 'Roboto', sans-serif;
            padding-left: 0;
            padding-right: 0;
            padding-top: 72px;
            padding-bottom: 48px;
          ""
          valign=""top""
        >
          <a
            data-click-track-id=""3182""
            href=""http://crel.site""
            style=""color: #3999c1 !important""
            title=""crelSite""
            ><img
              alt=""Similar Web""
              class=""1ex""
              height=""auto""
              src=""https://i.ibb.co/6vD02sj/crel-full-color-tb.png""
              style=""
                width: 140px;
                line-height: 100%;
                border: 0;
                outline: none;
                text-decoration: none;
              ""
              width=""159""
          /></a>
        </td>
      </tr>
      <tr>
        <td
          align=""center""
          style=""
            border-collapse: collapse !important;
            mso-table-lspace: 0pt;
            mso-table-rspace: 0pt;
            font-family: 'Roboto', sans-serif;
            padding-left: 0;
            padding-right: 0;
            padding-top: 0;
            padding-bottom: 0;
          ""
          valign=""top""
        >
          <!-- BODY -->
          <div
            style=""
              border: 1px solid rgba(223, 226, 230, 0.6);
              border-radius: 4px;
              background-image: url(https://i.ibb.co/wLFPWrc/3891942.jpg);
              background-repeat: no-repeat;
              background-position: bottom -3px right -3px;
              background-size: 36%;
            ""
          >
            <table
              class=""container""
              style=""
                border-collapse: collapse !important;
                mso-table-lspace: 0pt;
                mso-table-rspace: 0pt;
                font-family: 'Roboto', sans-serif;
                padding-left: 0;
                padding-right: 0;
                padding-top: 0;
                padding-bottom: 0;
                width: 100%;
                max-width: 600px;
                margin: 0 auto;
                padding: 0;
                clear: both;
              ""
              width=""100%""
            >
              <tr>
                <td
                  style=""
                    border-collapse: collapse !important;
                    mso-table-lspace: 0pt;
                    mso-table-rspace: 0pt;
                    font-family: 'Roboto', sans-serif;
                    padding-left: 0;
                    padding-right: 0;
                    padding-top: 0;
                    padding-bottom: 0;
                  ""
                >
                  <table
                    class=""row""
                    style=""
                      border-collapse: collapse !important;
                      mso-table-lspace: 0pt;
                      mso-table-rspace: 0pt;
                      font-family: 'Roboto', sans-serif;
                      padding-left: 0;
                      padding-right: 0;
                      padding-top: 0;
                      padding-bottom: 0;
                      width: 100%;
                    ""
                    width=""100%""
                  >
                    <tr>
                      <td
                        class=""content""
                        style=""
                          border-collapse: collapse !important;
                          mso-table-lspace: 0pt;
                          mso-table-rspace: 0pt;
                          font-family: 'Roboto', sans-serif;
                          padding-right: 48px;
                          padding-bottom: 48px;
                          padding-left: 48px;
                        ""
                      >
                        <table
                          class=""row""
                          style=""
                            border-collapse: collapse !important;
                            mso-table-lspace: 0pt;
                            mso-table-rspace: 0pt;
                            font-family: 'Roboto', sans-serif;
                            padding-left: 0;
                            padding-right: 0;
                            padding-top: 0;
                            padding-bottom: 0;
                            width: 100%;
                          ""
                          width=""100%""
                        >
                          <tr>
                            <td
                              style=""
                                border-collapse: collapse !important;
                                mso-table-lspace: 0pt;
                                mso-table-rspace: 0pt;
                                font-family: 'Roboto', sans-serif;
                                padding-left: 0;
                                padding-right: 0;
                                color: #2a3e52;
                                padding-top: 16px;
                                padding-bottom: 0px;
                              ""
                            >
                            <p
                                style=""
                                  padding: 0 15px 10px;
                                  color: #474e52;
                                  font-family: Montserrat, Roboto, Arial,
                                    sans-serif;
                                  font-size: 24px;
                                  line-height: 30px;
                                  text-align: left;
                                  font-weight: 700;
                                ""
                              >
                              Ch??o <CrelUserName>,
                              </p>
                              <p
                                style=""
                                  padding: 0 15px 10px;
                                  color: #474e52;
                                  font-family: Montserrat, Roboto, Arial,
                                    sans-serif;
                                  font-size: 24px;
                                  line-height: 30px;
                                  text-align: left;
                                  font-weight: 700;
                                ""
                              >
                                Ch??ng t??i ngh?? nh???ng m???t b???ng b??n d?????i s??? ph??
                                h???p v???i b???n
                              </p>

                              <br />
                              <table
                                width=""100%""
                                border=""0""
                                cellspacing=""0""
                                cellpadding=""0""
                              >
                                <tbody>
                                  <CrelPropertyList>
                                </tbody>
                              </table>
                              <br />
                              <p
                                style=""
                                  padding: 0 15px 10px;
                                  color: #474e52;
                                  font-family: Montserrat, Roboto, Arial,
                                    sans-serif;
                                  font-size: 16px;
                                  text-align: left;
                                  font-weight: 500;
                                ""
                              >
                                C???m ??n ???? ????? l???i y??u c???u t??m ki???m tr??n website
                                c???a th???ng t??i, chi ti???t ????? xu???t xin truy c???p
                                trang web:
                                <a href=""https://crel.site"">crel.site</a>.
                              </p>
                            </td>
                          </tr>

                          <tr>
                            <td
                              align=""center""
                              style=""
                                border-collapse: collapse !important;
                                mso-table-lspace: 0pt;
                                mso-table-rspace: 0pt;
                                font-family: 'Roboto', sans-serif;
                                padding-left: 0;
                                padding-right: 0;
                                padding-bottom: 0;
                                color: #000;
                                font-weight: bold;
                                font-size: 24px;
                                padding-top: 35px;
                                padding-bottom: 35px;
                                text-align: center;
                              ""
                            ></td>
                          </tr>
                          <tr>
                            <td
                              style=""
                                padding: 0 15px 10px;
                                color: #474e52;
                                font-family: Montserrat, Roboto, Arial,
                                  sans-serif;
                                font-size: 16px;
                                text-align: left;
                                font-weight: 500;
                              ""
                            >
                              Ch??c b???n c?? m???t ng??y tuy???t v???i, <br />CREL
                            </td>
                          </tr>
                          <tr>
                            <td
                              style=""
                                border-collapse: collapse !important;
                                mso-table-lspace: 0pt;
                                mso-table-rspace: 0pt;
                                padding-left: 0;
                                padding-top: 0;
                                padding-bottom: 0;
                                font-family: 'Roboto', sans-serif;
                                font-size: 14px;
                                line-height: 16px;
                                padding-right: 80px;
                              ""
                            >
                              C???n gi??p ?????? H??y g???i mail t???i
                              <span
                                style=""
                                  color: #4f8df9 !important;
                                  text-decoration: none;
                                ""
                                target=""_blank""
                                >crel2022@gmail.com</span
                              >
                            </td>
                          </tr>
                        </table>
                      </td>
                    </tr>
                  </table>
                </td>
              </tr>
            </table>
          </div>

          <!-- BODY END -->
        </td>
      </tr>
    </table>
  </body>
</html>

";

    public const string htmlPropertyListTemplate = @"
<tr>
                                    <td
                                      width=""17""
                                      style=""
                                        font-size: 0pt;
                                        line-height: 0pt;
                                        text-align: left;
                                        margin-left: 1rem;
                                        padding-top: 1rem;
                                        padding-bottom: 1rem;
                                      ""
                                    ></td>
                                    <td valign=""top"" width=""164"" style=""padding-top: 1rem;
                                    padding-bottom: 1rem;"">
                                      <a
                                        href=""<CrelLink>""
                                        target=""_blank""
                                      >
                                        <table
                                          width=""100%""
                                          border=""0""
                                          cellspacing=""0""
                                          cellpadding=""0""
                                        >
                                          <tbody>
                                            <tr>
                                              <td style=""padding-bottom: 6px"">
                                                <table
                                                  width=""100%""
                                                  border=""0""
                                                  cellspacing=""0""
                                                  cellpadding=""0""
                                                >
                                                  <tbody>
                                                    <tr>
                                                      <td
                                                        background=""<CrelImageLink>""
                                                        bgcolor=""#adc0c3""
                                                        style=""
                                                          border-radius: 8px;
                                                          background-size: cover;
                                                          background-repeat: no-repeat;
                                                        ""
                                                        valign=""top""
                                                        height=""225""
                                                      >
                                                        <div>
                                                          <table
                                                            width=""100%""
                                                            border=""0""
                                                            cellspacing=""0""
                                                            cellpadding=""0""
                                                          >
                                                            <tbody>
                                                              <tr>
                                                                <td
                                                                  valign=""top""
                                                                  width=""4""
                                                                  height=""122""
                                                                  style=""
                                                                    font-size: 0pt;
                                                                    line-height: 0pt;
                                                                    text-align: left;
                                                                  ""
                                                                ></td>
                                                                <td
                                                                  valign=""top""
                                                                  style=""
                                                                    padding: 4px
                                                                      0px;
                                                                  ""
                                                                >
                                                                  <table
                                                                    border=""0""
                                                                    cellspacing=""0""
                                                                    cellpadding=""0""
                                                                  >
                                                                    <tbody>
                                                                      <tr>
                                                                        <td
                                                                          style=""
                                                                            color: #000c76;
                                                                            border-radius: 4px;
                                                                            font-family: Montserrat,
                                                                              Roboto,
                                                                              Arial,
                                                                              sans-serif;
                                                                            font-size: 10px;
                                                                            background: #ffffff;
                                                                            line-height: 14px;
                                                                            padding: 2px
                                                                              10px;
                                                                            text-align: center;
                                                                            font-weight: bold;
                                                                            min-width: auto !important;
                                                                          ""
                                                                        >
                                                                          ???????c
                                                                          ?????
                                                                          xu???t
                                                                        </td>
                                                                      </tr>
                                                                    </tbody>
                                                                  </table>
                                                                </td>
                                                                <td
                                                                  width=""4""
                                                                  style=""
                                                                    font-size: 0pt;
                                                                    line-height: 0pt;
                                                                    text-align: left;
                                                                  ""
                                                                ></td>
                                                              </tr>
                                                            </tbody>
                                                          </table>
                                                        </div>
                                                      </td>
                                                    </tr>
                                                  </tbody>
                                                </table>
                                              </td>
                                            </tr>
                                            <tr>
                                              <td>
                                                <table
                                                  width=""100%""
                                                  border=""0""
                                                  cellspacing=""0""
                                                  cellpadding=""0""
                                                >
                                                  <tbody>
                                                    <tr>
                                                      <td
                                                        style=""
                                                          padding-bottom: 4px;
                                                          color: #474e52;
                                                          font-family: Montserrat,
                                                            Roboto, Arial,
                                                            sans-serif;
                                                          font-size: 18px;
                                                          line-height: 24px;
                                                          text-align: left;
                                                          font-weight: 700;
                                                        ""
                                                      >
                                                        <span
                                                          class=""m_3073352769970730789nolink""
                                                          style=""
                                                            color: #3b4044;
                                                            text-decoration: none;
                                                          ""
                                                          ><CrelPrice>???</span
                                                        >
                                                      </td>
                                                    </tr>
                                                    <tr>
                                                      <td
                                                        style=""
                                                          padding-bottom: 8px;
                                                          color: #3b4144;
                                                          font-family: Roboto,
                                                            Arial, sans-serif;
                                                          font-size: 16px;
                                                          line-height: 24px;
                                                          text-align: left;
                                                          font-weight: normal;
                                                          min-width: auto !important;
                                                        ""
                                                      >
                                                      <CrelAddress>
                                                      </td>
                                                    </tr>
                                                  </tbody>
                                                </table>
                                              </td>
                                            </tr>
                                          </tbody>
                                        </table>
                                      </a>
                                    </td>
                                    <td
                                      width=""17""
                                      style=""
                                        font-size: 0pt;
                                        line-height: 0pt;
                                        text-align: left;
                                        margin-left: 1rem;
                                        padding-top: 1rem;
                                        padding-bottom: 1rem;
                                      ""
                                    ></td>
                                  </tr>
";

    public const string htmlBrandRequestTemplate = @"
<!DOCTYPE html>
<html>
  <head>
    <style type=""text/css"">
      a .yshortcuts:hover {
        background-color: transparent !important;
        border: none !important;
        color: inherit !important;
      }
      a .yshortcuts:active {
        background-color: transparent !important;
        border: none !important;
        color: inherit !important;
      }
      a .yshortcuts:focus {
        background-color: transparent !important;
        border: none !important;
        color: inherit !important;
      }
    </style>
    <style media=""only screen and (max-width: 520px)"" type=""text/css"">
      /* /\/\/\/\/\/\/\/\/ RESPONSIVE MOJO /\/\/\/\/\/\/\/\/ */
      /*  must escape media query with double symbol */
      @media only screen and (max-width: 520px) {
        .main-table {
          width: 90% !important;
        }
        .top {
          padding-top: 33px !important;
          padding-bottom: 37px !important;
        }
        .content {
          padding: 24px 29px !important;
        }
        .verify-button {
          padding: 25px 0 !important;
        }
      }
    </style>
  </head>
  <body
    align=""center""
    style=""
      margin: 0;
      padding: 0;
      -webkit-text-size-adjust: 100%;
      -ms-text-size-adjust: 100%;
      background: #ffffff;
      width: 100%;
      font-family: 'Roboto', sans-serif;
      font-size: 16px;
      text-align: center;
      line-height: 22px;
      color: #aab2ba;
    ""
    width=""100%""
  >
    <table
      class=""main-table""
      height=""100%""
      style=""
        border-collapse: collapse !important;
        mso-table-lspace: 0pt;
        mso-table-rspace: 0pt;
        font-family: 'Roboto', sans-serif;
        padding-left: 0;
        padding-right: 0;
        padding-top: 0;
        padding-bottom: 0;
        margin: 20px auto 10px;
        padding: 0;
        height: 100%;
        width: 80%;
        max-width: 600px;
      ""
      width=""80%""
    >
      <tr>
        <td
          align=""center""
          class=""top""
          style=""
            border-collapse: collapse !important;
            mso-table-lspace: 0pt;
            mso-table-rspace: 0pt;
            font-family: 'Roboto', sans-serif;
            padding-left: 0;
            padding-right: 0;
            padding-top: 72px;
            padding-bottom: 48px;
          ""
          valign=""top""
        >
          <a
            data-click-track-id=""3182""
            href=""http://crel.site""
            style=""color: #3999c1 !important""
            title=""crelSite""
            ><img
              alt=""Similar Web""
              class=""1ex""
              height=""auto""
              src=""https://i.ibb.co/6vD02sj/crel-full-color-tb.png""
              style=""
                width: 140px;
                line-height: 100%;
                border: 0;
                outline: none;
                text-decoration: none;
              ""
              width=""159""
          /></a>
        </td>
      </tr>
      <tr>
        <td
          align=""center""
          style=""
            border-collapse: collapse !important;
            mso-table-lspace: 0pt;
            mso-table-rspace: 0pt;
            font-family: 'Roboto', sans-serif;
            padding-left: 0;
            padding-right: 0;
            padding-top: 0;
            padding-bottom: 0;
          ""
          valign=""top""
        >
          <!-- BODY -->
          <div
            style=""
              border: 1px solid rgba(223, 226, 230, 0.6);
              border-radius: 4px;
              background-image: url(https://i.ibb.co/wLFPWrc/3891942.jpg);
              background-repeat: no-repeat;
              background-position: bottom -3px right -3px;
              background-size: 36%;
            ""
          >
            <table
              class=""container""
              style=""
                border-collapse: collapse !important;
                mso-table-lspace: 0pt;
                mso-table-rspace: 0pt;
                font-family: 'Roboto', sans-serif;
                padding-left: 0;
                padding-right: 0;
                padding-top: 0;
                padding-bottom: 0;
                width: 100%;
                max-width: 600px;
                margin: 0 auto;
                padding: 0;
                clear: both;
              ""
              width=""100%""
            >
              <tr>
                <td
                  style=""
                    border-collapse: collapse !important;
                    mso-table-lspace: 0pt;
                    mso-table-rspace: 0pt;
                    font-family: 'Roboto', sans-serif;
                    padding-left: 0;
                    padding-right: 0;
                    padding-top: 0;
                    padding-bottom: 0;
                  ""
                >
                  <table
                    class=""row""
                    style=""
                      border-collapse: collapse !important;
                      mso-table-lspace: 0pt;
                      mso-table-rspace: 0pt;
                      font-family: 'Roboto', sans-serif;
                      padding-left: 0;
                      padding-right: 0;
                      padding-top: 0;
                      padding-bottom: 0;
                      width: 100%;
                    ""
                    width=""100%""
                  >
                    <tr>
                      <td
                        class=""content""
                        style=""
                          border-collapse: collapse !important;
                          mso-table-lspace: 0pt;
                          mso-table-rspace: 0pt;
                          font-family: 'Roboto', sans-serif;
                          padding-right: 48px;
                          padding-bottom: 48px;
                          padding-left: 48px;
                        ""
                      >
                        <table
                          class=""row""
                          style=""
                            border-collapse: collapse !important;
                            mso-table-lspace: 0pt;
                            mso-table-rspace: 0pt;
                            font-family: 'Roboto', sans-serif;
                            padding-left: 0;
                            padding-right: 0;
                            padding-top: 0;
                            padding-bottom: 0;
                            width: 100%;
                          ""
                          width=""100%""
                        >
                          <tr>
                            <td
                              style=""
                                border-collapse: collapse !important;
                                mso-table-lspace: 0pt;
                                mso-table-rspace: 0pt;
                                font-family: 'Roboto', sans-serif;
                                padding-left: 0;
                                padding-right: 0;
                                color: #2a3e52;
                                padding-top: 16px;
                                padding-bottom: 0px;
                              ""
                            >
                              <p
                                style=""
                                  padding: 0 15px 10px;
                                  color: #474e52;
                                  font-family: Montserrat, Roboto, Arial,
                                    sans-serif;
                                  font-size: 24px;
                                  line-height: 30px;
                                  text-align: left;
                                  font-weight: 700;
                                ""
                              >
                              Ch??o <CrelUserName>,
                              </p>
                              <p
                                style=""
                                  padding: 0 15px 10px;
                                  color: #474e52;
                                  font-family: Montserrat, Roboto, Arial,
                                    sans-serif;
                                  font-size: 24px;
                                  line-height: 30px;
                                  text-align: left;
                                  font-weight: 700;
                                ""
                              >
                              B???n ???? t??m ki???m b???t ?????ng s???n v???i th??ng tin
                              </p>


                              <br style=""border: 1px solid #e6e8e6;"" />
                              <p
                                style=""
                                  padding: 0 15px 10px;
                                  color: #474e52;
                                  font-family: Montserrat, Roboto, Arial,
                                    sans-serif;
                                  font-size: 18px;
                                  line-height: 30px;
                                  text-align: left;
                                  font-weight: 700;
                                ""
                              >
                                Ch??ng t??i ngh?? nh???ng m???t b???ng b??n d?????i s??? ph??
                                h???p v???i b???n
                              </p>
                              <table
                              class=""row""
                              style=""
                                border-collapse: collapse !important;
                                mso-table-lspace: 0pt;
                                mso-table-rspace: 0pt;
                                font-family: 'Roboto', sans-serif;
                                padding-left: 0;
                                padding-right: 0;
                                padding-top: 0;
                                padding-bottom: 0;
                                width: 100%;
                              ""
                              width=""100%""
                            >
                            <tr>
                              <td style=""padding-left: 15px;"">Qu???n/Huy???n: </td>
                              <td style=""padding-left: 15px;""><CrelArea></td>
                            </tr>
                            <tr>
                              <td style=""padding-left: 15px;"">Kho???ng gi??: </td>
                              <td style=""padding-left: 15px;""><CrelMinPrice> - <CrelMaxPrice> tri???u/th??ng</td>
                            </tr>
                            <tr>
                              <td style=""padding-left: 15px;"">Kho???ng di???n t??ch: </td>
                              <td style=""padding-left: 15px;""><CrelMinFloorArea> - <CrelMaxFloorArea> m??</td>
                            </tr>
                            <tr>
                              <td style=""padding-left: 15px;"">M?? t???:</td>
                              <td style=""padding-left: 15px;""><CrelDescription></td>
                            </tr>
                          </table>
                              <br />
                              <table
                                width=""100%""
                                border=""0""
                                cellspacing=""0""
                                cellpadding=""0""
                              >
                                <tbody>
                                  <CrelPropertyList>
                                </tbody>
                              </table>
                              <br />
                              <p
                                style=""
                                  padding: 0 15px 10px;
                                  color: #474e52;
                                  font-family: Montserrat, Roboto, Arial,
                                    sans-serif;
                                  font-size: 16px;
                                  text-align: left;
                                  font-weight: 500;
                                ""
                              >
                                C???m ??n ???? ????? l???i y??u c???u t??m ki???m tr??n website
                                c???a th???ng t??i, chi ti???t ????? xu???t xin truy c???p
                                trang web:
                                <a href=""https://crel.site"">crel.site</a>.
                              </p>
                            </td>
                          </tr>

                          <tr>
                            <td
                              align=""center""
                              style=""
                                border-collapse: collapse !important;
                                mso-table-lspace: 0pt;
                                mso-table-rspace: 0pt;
                                font-family: 'Roboto', sans-serif;
                                padding-left: 0;
                                padding-right: 0;
                                padding-bottom: 0;
                                color: #000;
                                font-weight: bold;
                                font-size: 24px;
                                padding-top: 35px;
                                padding-bottom: 35px;
                                text-align: center;
                              ""
                            ></td>
                          </tr>
                          <tr>
                            <td
                              style=""
                                padding: 0 15px 10px;
                                color: #474e52;
                                font-family: Montserrat, Roboto, Arial,
                                  sans-serif;
                                font-size: 16px;
                                text-align: left;
                                font-weight: 500;
                              ""
                            >
                              Ch??c b???n c?? m???t ng??y tuy???t v???i, <br />CREL
                            </td>
                          </tr>
                          <tr>
                            <td
                              style=""
                                border-collapse: collapse !important;
                                mso-table-lspace: 0pt;
                                mso-table-rspace: 0pt;
                                padding-left: 0;
                                padding-top: 0;
                                padding-bottom: 0;
                                font-family: 'Roboto', sans-serif;
                                font-size: 14px;
                                line-height: 16px;
                                padding-right: 80px;
                              ""
                            >
                              C???n gi??p ?????? H??y g???i mail t???i
                              <span
                                style=""
                                  color: #4f8df9 !important;
                                  text-decoration: none;
                                ""
                                target=""_blank""
                                >crel2022@gmail.com</span
                              >
                            </td>
                          </tr>
                        </table>
                      </td>
                    </tr>
                  </table>
                </td>
              </tr>
            </table>
          </div>

          <!-- BODY END -->
        </td>
      </tr>
    </table>
  </body>
</html>

";

    public const string htmlBrandVerifiedTemplate = @"
<!DOCTYPE html>
<html>
  <head>
    <style type=""text/css"">
      a .yshortcuts:hover {
        background-color: transparent !important;
        border: none !important;
        color: inherit !important;
      }
      a .yshortcuts:active {
        background-color: transparent !important;
        border: none !important;
        color: inherit !important;
      }
      a .yshortcuts:focus {
        background-color: transparent !important;
        border: none !important;
        color: inherit !important;
      }
    </style>
    <style media=""only screen and (max-width: 520px)"" type=""text/css"">
      /* /\/\/\/\/\/\/\/\/ RESPONSIVE MOJO /\/\/\/\/\/\/\/\/ */
      /*  must escape media query with double symbol */
      @media only screen and (max-width: 520px) {
        .main-table {
          width: 90% !important;
        }
        .top {
          padding-top: 33px !important;
          padding-bottom: 37px !important;
        }
        .content {
          padding: 24px 29px !important;
        }
        .verify-button {
          padding: 25px 0 !important;
        }
      }
    </style>
  </head>
  <body
    align=""center""
    style=""
      margin: 0;
      padding: 0;
      -webkit-text-size-adjust: 100%;
      -ms-text-size-adjust: 100%;
      background: #ffffff;
      width: 100%;
      font-family: 'Roboto', sans-serif;
      font-size: 16px;
      text-align: center;
      line-height: 22px;
      color: #aab2ba;
    ""
    width=""100%""
  >
    <table
      class=""main-table""
      height=""100%""
      style=""
        border-collapse: collapse !important;
        mso-table-lspace: 0pt;
        mso-table-rspace: 0pt;
        font-family: 'Roboto', sans-serif;
        padding-left: 0;
        padding-right: 0;
        padding-top: 0;
        padding-bottom: 0;
        margin: 20px auto 10px;
        padding: 0;
        height: 100%;
        width: 80%;
        max-width: 600px;
      ""
      width=""80%""
    >
      <tr>
        <td
          align=""center""
          class=""top""
          style=""
            border-collapse: collapse !important;
            mso-table-lspace: 0pt;
            mso-table-rspace: 0pt;
            font-family: 'Roboto', sans-serif;
            padding-left: 0;
            padding-right: 0;
            padding-top: 72px;
            padding-bottom: 48px;
          ""
          valign=""top""
        >
          <a
            data-click-track-id=""3182""
            href=""http://crel.site""
            style=""color: #3999c1 !important""
            title=""crelSite""
            ><img
              alt=""Similar Web""
              class=""1ex""
              height=""auto""
              src=""https://i.ibb.co/6vD02sj/crel-full-color-tb.png""
              style=""
                width: 140px;
                line-height: 100%;
                border: 0;
                outline: none;
                text-decoration: none;
              ""
              width=""159""
          /></a>
        </td>
      </tr>
      <tr>
        <td
          align=""center""
          style=""
            border-collapse: collapse !important;
            mso-table-lspace: 0pt;
            mso-table-rspace: 0pt;
            font-family: 'Roboto', sans-serif;
            padding-left: 0;
            padding-right: 0;
            padding-top: 0;
            padding-bottom: 0;
          ""
          valign=""top""
        >
          <!-- BODY -->
          <div
            style=""
              border: 1px solid rgba(223, 226, 230, 0.6);
              border-radius: 4px;
              background-image: url(https://i.ibb.co/F8H1jyT/4957136.jpg);
              background-repeat: no-repeat;
              background-position: bottom -3px right -3px;
              background-size: 36%;
            ""
          >
            <table
              class=""container""
              style=""
                border-collapse: collapse !important;
                mso-table-lspace: 0pt;
                mso-table-rspace: 0pt;
                font-family: 'Roboto', sans-serif;
                padding-left: 0;
                padding-right: 0;
                padding-top: 0;
                padding-bottom: 0;
                width: 100%;
                max-width: 600px;
                margin: 0 auto;
                padding: 0;
                clear: both;
              ""
              width=""100%""
            >
              <tr>
                <td
                  style=""
                    border-collapse: collapse !important;
                    mso-table-lspace: 0pt;
                    mso-table-rspace: 0pt;
                    font-family: 'Roboto', sans-serif;
                    padding-left: 0;
                    padding-right: 0;
                    padding-top: 0;
                    padding-bottom: 0;
                  ""
                >
                  <table
                    class=""row""
                    style=""
                      border-collapse: collapse !important;
                      mso-table-lspace: 0pt;
                      mso-table-rspace: 0pt;
                      font-family: 'Roboto', sans-serif;
                      padding-left: 0;
                      padding-right: 0;
                      padding-top: 0;
                      padding-bottom: 0;
                      width: 100%;
                    ""
                    width=""100%""
                  >
                    <tr>
                      <td
                        class=""content""
                        style=""
                          border-collapse: collapse !important;
                          mso-table-lspace: 0pt;
                          mso-table-rspace: 0pt;
                          font-family: 'Roboto', sans-serif;
                          padding-top: 48px;
                          padding-right: 48px;
                          padding-bottom: 48px;
                          padding-left: 48px;
                        ""
                      >
                        <table
                          class=""row""
                          style=""
                            border-collapse: collapse !important;
                            mso-table-lspace: 0pt;
                            mso-table-rspace: 0pt;
                            font-family: 'Roboto', sans-serif;
                            padding-left: 0;
                            padding-right: 0;
                            padding-top: 0;
                            padding-bottom: 0;
                            width: 100%;
                          ""
                          width=""100%""
                        >
                          <tr>
                            <td
                              style=""
                                border-collapse: collapse !important;
                                mso-table-lspace: 0pt;
                                mso-table-rspace: 0pt;
                                padding-left: 0;
                                padding-right: 0;
                                padding-top: 0;
                                padding-bottom: 0;
                                font-family: 'Roboto', sans-serif;
                                font-size: 24px;
                                line-height: 38px;
                                color: #1b2653;
                              ""
                            >
                              Xin ch??o <CrelName>,
                            </td>
                          </tr>
                          <tr>
                            <td
                              style=""
                                border-collapse: collapse !important;
                                mso-table-lspace: 0pt;
                                mso-table-rspace: 0pt;
                                font-family: 'Roboto', sans-serif;
                                padding-left: 0;
                                padding-right: 0;
                                color: #2a3e52;
                                padding-top: 16px;
                                padding-bottom: 0px;
                              ""
                            >
                              T??i kho???n <strong><CrelName></strong> ???? ???????c x??c th???c!.<br/>
                              Hi???n t???i t??i kho???n <strong><CrelName></strong> ???? c?? th??? ????ng nh???p v?? s??? d???ng t???t c??? d???ch v???
                              t???i website: <a href='https://crel.site'>crel.site</a>.
                            </td>
                          </tr>
                          
                          <tr>
                            <td
                              align=""center""
                              style=""
                                border-collapse: collapse !important;
                                mso-table-lspace: 0pt;
                                mso-table-rspace: 0pt;
                                font-family: 'Roboto', sans-serif;
                                padding-left: 0;
                                padding-right: 0;
                                padding-bottom: 0;
                                color: #000;
                                font-weight: bold;
                                font-size: 24px;
                                padding-top: 35px;
                                padding-bottom: 35px;
                                text-align: center;
                              ""
                            >
                              
                            </td>
                          </tr>
                          <tr>
                            <td
                              style=""
                                border-collapse: collapse !important;
                                mso-table-lspace: 0pt;
                                mso-table-rspace: 0pt;
                                color: #2a3e52;
                                font-family: 'Roboto', sans-serif;
                                font-size: 16px;
                                line-height: 22px;
                                padding-top: 0px;
                                padding-right: 0px;
                                padding-bottom: 26px;
                                padding-left: 0;
                              ""
                            >
                              Ch??c b???n c?? m???t ng??y tuy???t v???i, <br />CREL
                            </td>
                          </tr>
                          <tr>
                            <td
                              style=""
                                border-collapse: collapse !important;
                                mso-table-lspace: 0pt;
                                mso-table-rspace: 0pt;
                                padding-left: 0;
                                padding-top: 0;
                                padding-bottom: 0;
                                font-family: 'Roboto', sans-serif;
                                font-size: 14px;
                                line-height: 16px;
                                padding-right: 80px;
                              ""
                            >
                              C???n gi??p ?????? H??y g???i mail t???i
                              <span
                                style=""
                                  color: #4f8df9 !important;
                                  text-decoration: none;
                                ""
                                target=""_blank""
                                >crel2022@gmail.com</span
                              >
                            </td>
                          </tr>
                        </table>
                      </td>
                    </tr>
                  </table>
                </td>
              </tr>
            </table>
          </div>

          <!-- BODY END -->
        </td>
      </tr>
    </table>
  </body>
</html>

";

    public const string htmlBrandNotVerifiedTemplate = @"
<!DOCTYPE html>
<html>
  <head>
    <style type=""text/css"">
      a .yshortcuts:hover {
        background-color: transparent !important;
        border: none !important;
        color: inherit !important;
      }
      a .yshortcuts:active {
        background-color: transparent !important;
        border: none !important;
        color: inherit !important;
      }
      a .yshortcuts:focus {
        background-color: transparent !important;
        border: none !important;
        color: inherit !important;
      }
    </style>
    <style media=""only screen and (max-width: 520px)"" type=""text/css"">
      /* /\/\/\/\/\/\/\/\/ RESPONSIVE MOJO /\/\/\/\/\/\/\/\/ */
      /*  must escape media query with double symbol */
      @media only screen and (max-width: 520px) {
        .main-table {
          width: 90% !important;
        }
        .top {
          padding-top: 33px !important;
          padding-bottom: 37px !important;
        }
        .content {
          padding: 24px 29px !important;
        }
        .verify-button {
          padding: 25px 0 !important;
        }
      }
    </style>
  </head>
  <body
    align=""center""
    style=""
      margin: 0;
      padding: 0;
      -webkit-text-size-adjust: 100%;
      -ms-text-size-adjust: 100%;
      background: #ffffff;
      width: 100%;
      font-family: 'Roboto', sans-serif;
      font-size: 16px;
      text-align: center;
      line-height: 22px;
      color: #aab2ba;
    ""
    width=""100%""
  >
    <table
      class=""main-table""
      height=""100%""
      style=""
        border-collapse: collapse !important;
        mso-table-lspace: 0pt;
        mso-table-rspace: 0pt;
        font-family: 'Roboto', sans-serif;
        padding-left: 0;
        padding-right: 0;
        padding-top: 0;
        padding-bottom: 0;
        margin: 20px auto 10px;
        padding: 0;
        height: 100%;
        width: 80%;
        max-width: 600px;
      ""
      width=""80%""
    >
      <tr>
        <td
          align=""center""
          class=""top""
          style=""
            border-collapse: collapse !important;
            mso-table-lspace: 0pt;
            mso-table-rspace: 0pt;
            font-family: 'Roboto', sans-serif;
            padding-left: 0;
            padding-right: 0;
            padding-top: 72px;
            padding-bottom: 48px;
          ""
          valign=""top""
        >
          <a
            data-click-track-id=""3182""
            href=""http://crel.site""
            style=""color: #3999c1 !important""
            title=""crelSite""
            ><img
              alt=""Similar Web""
              class=""1ex""
              height=""auto""
              src=""https://i.ibb.co/6vD02sj/crel-full-color-tb.png""
              style=""
                width: 140px;
                line-height: 100%;
                border: 0;
                outline: none;
                text-decoration: none;
              ""
              width=""159""
          /></a>
        </td>
      </tr>
      <tr>
        <td
          align=""center""
          style=""
            border-collapse: collapse !important;
            mso-table-lspace: 0pt;
            mso-table-rspace: 0pt;
            font-family: 'Roboto', sans-serif;
            padding-left: 0;
            padding-right: 0;
            padding-top: 0;
            padding-bottom: 0;
          ""
          valign=""top""
        >
          <!-- BODY -->
          <div
            style=""
              border: 1px solid rgba(223, 226, 230, 0.6);
              border-radius: 4px;
              background-image: url(https://iili.io/HxapKu9.md.jpg);
              background-repeat: no-repeat;
              background-position: bottom -3px right -3px;
              background-size: 36%;
            ""
          >
            <table
              class=""container""
              style=""
                border-collapse: collapse !important;
                mso-table-lspace: 0pt;
                mso-table-rspace: 0pt;
                font-family: 'Roboto', sans-serif;
                padding-left: 0;
                padding-right: 0;
                padding-top: 0;
                padding-bottom: 0;
                width: 100%;
                max-width: 600px;
                margin: 0 auto;
                padding: 0;
                clear: both;
              ""
              width=""100%""
            >
              <tr>
                <td
                  style=""
                    border-collapse: collapse !important;
                    mso-table-lspace: 0pt;
                    mso-table-rspace: 0pt;
                    font-family: 'Roboto', sans-serif;
                    padding-left: 0;
                    padding-right: 0;
                    padding-top: 0;
                    padding-bottom: 0;
                  ""
                >
                  <table
                    class=""row""
                    style=""
                      border-collapse: collapse !important;
                      mso-table-lspace: 0pt;
                      mso-table-rspace: 0pt;
                      font-family: 'Roboto', sans-serif;
                      padding-left: 0;
                      padding-right: 0;
                      padding-top: 0;
                      padding-bottom: 0;
                      width: 100%;
                    ""
                    width=""100%""
                  >
                    <tr>
                      <td
                        class=""content""
                        style=""
                          border-collapse: collapse !important;
                          mso-table-lspace: 0pt;
                          mso-table-rspace: 0pt;
                          font-family: 'Roboto', sans-serif;
                          padding-top: 48px;
                          padding-right: 48px;
                          padding-bottom: 48px;
                          padding-left: 48px;
                        ""
                      >
                        <table
                          class=""row""
                          style=""
                            border-collapse: collapse !important;
                            mso-table-lspace: 0pt;
                            mso-table-rspace: 0pt;
                            font-family: 'Roboto', sans-serif;
                            padding-left: 0;
                            padding-right: 0;
                            padding-top: 0;
                            padding-bottom: 0;
                            width: 100%;
                          ""
                          width=""100%""
                        >
                          <tr>
                            <td
                              style=""
                                border-collapse: collapse !important;
                                mso-table-lspace: 0pt;
                                mso-table-rspace: 0pt;
                                padding-left: 0;
                                padding-right: 0;
                                padding-top: 0;
                                padding-bottom: 0;
                                font-family: 'Roboto', sans-serif;
                                font-size: 24px;
                                line-height: 38px;
                                color: #1b2653;
                              ""
                            >
                              Xin ch??o <CrelName>,
                            </td>
                          </tr>
                          <tr>
                            <td
                              style=""
                                border-collapse: collapse !important;
                                mso-table-lspace: 0pt;
                                mso-table-rspace: 0pt;
                                font-family: 'Roboto', sans-serif;
                                padding-left: 0;
                                padding-right: 0;
                                color: #2a3e52;
                                padding-top: 16px;
                                padding-bottom: 0px;
                              ""
                            >
                              T??i kho???n <strong><CrelName></strong> kh??ng ???????c duy???t!.<br/>
                                L?? do: <CrelReason> <br/>
                              
                                  Ch??ng t??i s??? s???m li??n h??? ????? t??m h?????ng x??? l?? th??ng qua ?????a ch??? email ho???c s??? ??i???n tho???i.
                            </td>
                          </tr>
                          
                          <tr>
                            <td
                              align=""center""
                              style=""
                                border-collapse: collapse !important;
                                mso-table-lspace: 0pt;
                                mso-table-rspace: 0pt;
                                font-family: 'Roboto', sans-serif;
                                padding-left: 0;
                                padding-right: 0;
                                padding-bottom: 0;
                                color: #000;
                                font-weight: bold;
                                font-size: 24px;
                                padding-top: 35px;
                                padding-bottom: 35px;
                                text-align: center;
                              ""
                            >
                              
                            </td>
                          </tr>
                          <tr>
                            <td
                              style=""
                                border-collapse: collapse !important;
                                mso-table-lspace: 0pt;
                                mso-table-rspace: 0pt;
                                color: #2a3e52;
                                font-family: 'Roboto', sans-serif;
                                font-size: 16px;
                                line-height: 22px;
                                padding-top: 0px;
                                padding-right: 0px;
                                padding-bottom: 26px;
                                padding-left: 0;
                              ""
                            >
                              Ch??c b???n c?? m???t ng??y tuy???t v???i, <br />CREL
                            </td>
                          </tr>
                          <tr>
                            <td
                              style=""
                                border-collapse: collapse !important;
                                mso-table-lspace: 0pt;
                                mso-table-rspace: 0pt;
                                padding-left: 0;
                                padding-top: 0;
                                padding-bottom: 0;
                                font-family: 'Roboto', sans-serif;
                                font-size: 14px;
                                line-height: 16px;
                                padding-right: 80px;
                              ""
                            >
                              C???n gi??p ?????? H??y g???i mail t???i
                              <span
                                style=""
                                  color: #4f8df9 !important;
                                  text-decoration: none;
                                ""
                                target=""_blank""
                                >crel2022@gmail.com</span
                              >
                            </td>
                          </tr>
                        </table>
                      </td>
                    </tr>
                  </table>
                </td>
              </tr>
            </table>
          </div>

          <!-- BODY END -->
        </td>
      </tr>
    </table>
  </body>
</html>

";

    public const string htmlBrokerApproveAppointmentTemplate = @"
<!DOCTYPE html>
<html>
  <head>
    <style type=""text/css"">
      a .yshortcuts:hover {
        background-color: transparent !important;
        border: none !important;
        color: inherit !important;
      }
      a .yshortcuts:active {
        background-color: transparent !important;
        border: none !important;
        color: inherit !important;
      }
      a .yshortcuts:focus {
        background-color: transparent !important;
        border: none !important;
        color: inherit !important;
      }
    </style>
    <style media=""only screen and (max-width: 520px)"" type=""text/css"">
      /* /\/\/\/\/\/\/\/\/ RESPONSIVE MOJO /\/\/\/\/\/\/\/\/ */
      /*  must escape media query with double symbol */
      @media only screen and (max-width: 520px) {
        .main-table {
          width: 90% !important;
        }
        .top {
          padding-top: 33px !important;
          padding-bottom: 37px !important;
        }
        .content {
          padding: 24px 29px !important;
        }
        .verify-button {
          padding: 25px 0 !important;
        }
      }
    </style>
  </head>
  <body
    align=""center""
    style=""
      margin: 0;
      padding: 0;
      -webkit-text-size-adjust: 100%;
      -ms-text-size-adjust: 100%;
      background: #ffffff;
      width: 100%;
      font-family: 'Roboto', sans-serif;
      font-size: 16px;
      text-align: center;
      line-height: 22px;
      color: #aab2ba;
    ""
    width=""100%""
  >
    <table
      class=""main-table""
      height=""100%""
      style=""
        border-collapse: collapse !important;
        mso-table-lspace: 0pt;
        mso-table-rspace: 0pt;
        font-family: 'Roboto', sans-serif;
        padding-left: 0;
        padding-right: 0;
        padding-top: 0;
        padding-bottom: 0;
        margin: 20px auto 10px;
        padding: 0;
        height: 100%;
        width: 80%;
        max-width: 600px;
      ""
      width=""80%""
    >
      <tr>
        <td
          align=""center""
          class=""top""
          style=""
            border-collapse: collapse !important;
            mso-table-lspace: 0pt;
            mso-table-rspace: 0pt;
            font-family: 'Roboto', sans-serif;
            padding-left: 0;
            padding-right: 0;
            padding-top: 72px;
            padding-bottom: 48px;
          ""
          valign=""top""
        >
          <a
            data-click-track-id=""3182""
            href=""http://crel.site""
            style=""color: #3999c1 !important""
            title=""crelSite""
            ><img
              alt=""Similar Web""
              class=""1ex""
              height=""auto""
              src=""https://i.ibb.co/6vD02sj/crel-full-color-tb.png""
              style=""
                width: 140px;
                line-height: 100%;
                border: 0;
                outline: none;
                text-decoration: none;
              ""
              width=""159""
          /></a>
        </td>
      </tr>
      <tr>
        <td
          align=""center""
          style=""
            border-collapse: collapse !important;
            mso-table-lspace: 0pt;
            mso-table-rspace: 0pt;
            font-family: 'Roboto', sans-serif;
            padding-left: 0;
            padding-right: 0;
            padding-top: 0;
            padding-bottom: 0;
          ""
          valign=""top""
        >
          <!-- BODY -->
          <div
            style=""
              border: 1px solid rgba(223, 226, 230, 0.6);
              border-radius: 4px;
              background-image: url(https://i.ibb.co/wLFPWrc/3891942.jpg);
              background-repeat: no-repeat;
              background-position: bottom -3px right -3px;
              background-size: 36%;
            ""
          >
            <table
              class=""container""
              style=""
                border-collapse: collapse !important;
                mso-table-lspace: 0pt;
                mso-table-rspace: 0pt;
                font-family: 'Roboto', sans-serif;
                padding-left: 0;
                padding-right: 0;
                padding-top: 0;
                padding-bottom: 0;
                width: 100%;
                max-width: 600px;
                margin: 0 auto;
                padding: 0;
                clear: both;
              ""
              width=""100%""
            >
              <tr>
                <td
                  style=""
                    border-collapse: collapse !important;
                    mso-table-lspace: 0pt;
                    mso-table-rspace: 0pt;
                    font-family: 'Roboto', sans-serif;
                    padding-left: 0;
                    padding-right: 0;
                    padding-top: 0;
                    padding-bottom: 0;
                  ""
                >
                  <table
                    class=""row""
                    style=""
                      border-collapse: collapse !important;
                      mso-table-lspace: 0pt;
                      mso-table-rspace: 0pt;
                      font-family: 'Roboto', sans-serif;
                      padding-left: 0;
                      padding-right: 0;
                      padding-top: 0;
                      padding-bottom: 0;
                      width: 100%;
                    ""
                    width=""100%""
                  >
                    <tr>
                      <td
                        class=""content""
                        style=""
                          border-collapse: collapse !important;
                          mso-table-lspace: 0pt;
                          mso-table-rspace: 0pt;
                          font-family: 'Roboto', sans-serif;
                          padding-top: 48px;
                          padding-right: 48px;
                          padding-bottom: 48px;
                          padding-left: 48px;
                        ""
                      >
                        <table
                          class=""row""
                          style=""
                            border-collapse: collapse !important;
                            mso-table-lspace: 0pt;
                            mso-table-rspace: 0pt;
                            font-family: 'Roboto', sans-serif;
                            padding-left: 0;
                            padding-right: 0;
                            padding-top: 0;
                            padding-bottom: 0;
                            width: 100%;
                          ""
                          width=""100%""
                        >
                          <tr>
                            <td
                              style=""
                                border-collapse: collapse !important;
                                mso-table-lspace: 0pt;
                                mso-table-rspace: 0pt;
                                padding-left: 0;
                                padding-right: 0;
                                padding-top: 0;
                                padding-bottom: 0;
                                font-family: 'Roboto', sans-serif;
                                font-size: 24px;
                                line-height: 38px;
                                color: #1b2653;
                              ""
                            >
                              Cu???c h???n t???o m???i th??nh c??ng!
                            </td>
                          </tr>
                          <tr>
                            <td
                              style=""
                                border-collapse: collapse !important;
                                mso-table-lspace: 0pt;
                                mso-table-rspace: 0pt;
                                font-family: 'Roboto', sans-serif;
                                padding-left: 0;
                                padding-right: 0;
                                color: #2a3e52;
                                padding-top: 16px;
                                padding-bottom: 0px;
                              ""
                            >
                              <p>Cu???c h???n m???i ???? ???????c t???o!.</p>
                              <br />
                              <p>Th??ng tin cu???c h???n:</p>
                              <p>- Th???i gian: CrelOnDateTime</p>
                              <p>- M?? t???: CrelDescription</p>
                              <br />
                              <p>Th??ng tin ng?????i m??i gi???i h??? tr???:</p>
                              <p>- H??? v?? t??n: CrelBrokerName</p>
                              <p>- S??? ??i???n tho???i: CrelPhoneNumber</p>
                              <br />

                              <a href=""CrelLink"">
                                <span
                                  style=""color: #00a3b1; text-decoration: none""
                                  >Xem chi ti???t</span
                                >
                              </a>
                              <br />
                              <br />
                              C???m ??n ???? ?????t cu???c h???n v???i ch??ng t??i, chi ti???t xin
                              truy c???p trang web:
                              <a href=""https://crel.site"">crel.site</a>.
                            </td>
                          </tr>

                          <tr>
                            <td
                              align=""center""
                              style=""
                                border-collapse: collapse !important;
                                mso-table-lspace: 0pt;
                                mso-table-rspace: 0pt;
                                font-family: 'Roboto', sans-serif;
                                padding-left: 0;
                                padding-right: 0;
                                padding-bottom: 0;
                                color: #000;
                                font-weight: bold;
                                font-size: 24px;
                                padding-top: 35px;
                                padding-bottom: 35px;
                                text-align: center;
                              ""
                            ></td>
                          </tr>
                          <tr>
                            <td
                              style=""
                                border-collapse: collapse !important;
                                mso-table-lspace: 0pt;
                                mso-table-rspace: 0pt;
                                color: #2a3e52;
                                font-family: 'Roboto', sans-serif;
                                font-size: 16px;
                                line-height: 22px;
                                padding-top: 0px;
                                padding-right: 0px;
                                padding-bottom: 26px;
                                padding-left: 0;
                              ""
                            >
                              Ch??c b???n c?? m???t ng??y tuy???t v???i, <br />CREL
                            </td>
                          </tr>
                          <tr>
                            <td
                              style=""
                                border-collapse: collapse !important;
                                mso-table-lspace: 0pt;
                                mso-table-rspace: 0pt;
                                padding-left: 0;
                                padding-top: 0;
                                padding-bottom: 0;
                                font-family: 'Roboto', sans-serif;
                                font-size: 14px;
                                line-height: 16px;
                                padding-right: 80px;
                              ""
                            >
                              C???n gi??p ?????? H??y g???i mail t???i
                              <span
                                style=""
                                  color: #4f8df9 !important;
                                  text-decoration: none;
                                ""
                                target=""_blank""
                                >crel2022@gmail.com</span
                              >
                            </td>
                          </tr>
                        </table>
                      </td>
                    </tr>
                  </table>
                </td>
              </tr>
            </table>
          </div>

          <!-- BODY END -->
        </td>
      </tr>
    </table>
  </body>
</html>

";

    public const string htmlBrokerNotApproveAppointmentTemplate = @"
<!DOCTYPE html>
<html>
  <head>
    <style type=""text/css"">
      a .yshortcuts:hover {
        background-color: transparent !important;
        border: none !important;
        color: inherit !important;
      }
      a .yshortcuts:active {
        background-color: transparent !important;
        border: none !important;
        color: inherit !important;
      }
      a .yshortcuts:focus {
        background-color: transparent !important;
        border: none !important;
        color: inherit !important;
      }
    </style>
    <style media=""only screen and (max-width: 520px)"" type=""text/css"">
      /* /\/\/\/\/\/\/\/\/ RESPONSIVE MOJO /\/\/\/\/\/\/\/\/ */
      /*  must escape media query with double symbol */
      @media only screen and (max-width: 520px) {
        .main-table {
          width: 90% !important;
        }
        .top {
          padding-top: 33px !important;
          padding-bottom: 37px !important;
        }
        .content {
          padding: 24px 29px !important;
        }
        .verify-button {
          padding: 25px 0 !important;
        }
      }
    </style>
  </head>
  <body
    align=""center""
    style=""
      margin: 0;
      padding: 0;
      -webkit-text-size-adjust: 100%;
      -ms-text-size-adjust: 100%;
      background: #ffffff;
      width: 100%;
      font-family: 'Roboto', sans-serif;
      font-size: 16px;
      text-align: center;
      line-height: 22px;
      color: #aab2ba;
    ""
    width=""100%""
  >
    <table
      class=""main-table""
      height=""100%""
      style=""
        border-collapse: collapse !important;
        mso-table-lspace: 0pt;
        mso-table-rspace: 0pt;
        font-family: 'Roboto', sans-serif;
        padding-left: 0;
        padding-right: 0;
        padding-top: 0;
        padding-bottom: 0;
        margin: 20px auto 10px;
        padding: 0;
        height: 100%;
        width: 80%;
        max-width: 600px;
      ""
      width=""80%""
    >
      <tr>
        <td
          align=""center""
          class=""top""
          style=""
            border-collapse: collapse !important;
            mso-table-lspace: 0pt;
            mso-table-rspace: 0pt;
            font-family: 'Roboto', sans-serif;
            padding-left: 0;
            padding-right: 0;
            padding-top: 72px;
            padding-bottom: 48px;
          ""
          valign=""top""
        >
          <a
            data-click-track-id=""3182""
            href=""http://crel.site""
            style=""color: #3999c1 !important""
            title=""crelSite""
            ><img
              alt=""Similar Web""
              class=""1ex""
              height=""auto""
              src=""https://i.ibb.co/6vD02sj/crel-full-color-tb.png""
              style=""
                width: 140px;
                line-height: 100%;
                border: 0;
                outline: none;
                text-decoration: none;
              ""
              width=""159""
          /></a>
        </td>
      </tr>
      <tr>
        <td
          align=""center""
          style=""
            border-collapse: collapse !important;
            mso-table-lspace: 0pt;
            mso-table-rspace: 0pt;
            font-family: 'Roboto', sans-serif;
            padding-left: 0;
            padding-right: 0;
            padding-top: 0;
            padding-bottom: 0;
          ""
          valign=""top""
        >
          <!-- BODY -->
          <div
            style=""
              border: 1px solid rgba(223, 226, 230, 0.6);
              border-radius: 4px;
              background-image: url(https://i.ibb.co/mG5dJSt/11104.jpg);
              background-repeat: no-repeat;
              background-position: bottom -3px right -3px;
              background-size: 36%;
            ""
          >
            <table
              class=""container""
              style=""
                border-collapse: collapse !important;
                mso-table-lspace: 0pt;
                mso-table-rspace: 0pt;
                font-family: 'Roboto', sans-serif;
                padding-left: 0;
                padding-right: 0;
                padding-top: 0;
                padding-bottom: 0;
                width: 100%;
                max-width: 600px;
                margin: 0 auto;
                padding: 0;
                clear: both;
              ""
              width=""100%""
            >
              <tr>
                <td
                  style=""
                    border-collapse: collapse !important;
                    mso-table-lspace: 0pt;
                    mso-table-rspace: 0pt;
                    font-family: 'Roboto', sans-serif;
                    padding-left: 0;
                    padding-right: 0;
                    padding-top: 0;
                    padding-bottom: 0;
                  ""
                >
                  <table
                    class=""row""
                    style=""
                      border-collapse: collapse !important;
                      mso-table-lspace: 0pt;
                      mso-table-rspace: 0pt;
                      font-family: 'Roboto', sans-serif;
                      padding-left: 0;
                      padding-right: 0;
                      padding-top: 0;
                      padding-bottom: 0;
                      width: 100%;
                    ""
                    width=""100%""
                  >
                    <tr>
                      <td
                        class=""content""
                        style=""
                          border-collapse: collapse !important;
                          mso-table-lspace: 0pt;
                          mso-table-rspace: 0pt;
                          font-family: 'Roboto', sans-serif;
                          padding-top: 48px;
                          padding-right: 48px;
                          padding-bottom: 48px;
                          padding-left: 48px;
                        ""
                      >
                        <table
                          class=""row""
                          style=""
                            border-collapse: collapse !important;
                            mso-table-lspace: 0pt;
                            mso-table-rspace: 0pt;
                            font-family: 'Roboto', sans-serif;
                            padding-left: 0;
                            padding-right: 0;
                            padding-top: 0;
                            padding-bottom: 0;
                            width: 100%;
                          ""
                          width=""100%""
                        >
                          <tr>
                            <td
                              style=""
                                border-collapse: collapse !important;
                                mso-table-lspace: 0pt;
                                mso-table-rspace: 0pt;
                                padding-left: 0;
                                padding-right: 0;
                                padding-top: 0;
                                padding-bottom: 0;
                                font-family: 'Roboto', sans-serif;
                                font-size: 24px;
                                line-height: 38px;
                                color: #1b2653;
                              ""
                            >
                              Cu???c h???n t???o m???i th???t b???i,
                            </td>
                          </tr>
                          <tr>
                            <td
                              style=""
                                border-collapse: collapse !important;
                                mso-table-lspace: 0pt;
                                mso-table-rspace: 0pt;
                                font-family: 'Roboto', sans-serif;
                                padding-left: 0;
                                padding-right: 0;
                                color: #2a3e52;
                                padding-top: 16px;
                                padding-bottom: 0px;
                              ""
                            >
                              Cu???c h???n b???n t???o v??o l??c: <CrelOnDateTime> <br />
                              b??? t??? ch???i x??t duy???t!.
                              <br />
                              L?? do t??? ch???i cu???c h???n: <CrelReason>
                              <br />
                              <a href=""CrelLink"">
                                <span
                                  style=""color: #00a3b1; text-decoration: none""
                                  >Xem chi ti???t</span
                                >
                              </a>
                              <br />
                              <br />
                              C???m ??n ???? ?????t cu???c h???n v???i ch??ng t??i, chi ti???t xin
                              truy c???p trang web:
                              <a href=""https://crel.site"">crel.site</a>.
                            </td>
                          </tr>

                          <tr>
                            <td
                              align=""center""
                              style=""
                                border-collapse: collapse !important;
                                mso-table-lspace: 0pt;
                                mso-table-rspace: 0pt;
                                font-family: 'Roboto', sans-serif;
                                padding-left: 0;
                                padding-right: 0;
                                padding-bottom: 0;
                                color: #000;
                                font-weight: bold;
                                font-size: 24px;
                                padding-top: 35px;
                                padding-bottom: 35px;
                                text-align: center;
                              ""
                            ></td>
                          </tr>
                          <tr>
                            <td
                              style=""
                                border-collapse: collapse !important;
                                mso-table-lspace: 0pt;
                                mso-table-rspace: 0pt;
                                color: #2a3e52;
                                font-family: 'Roboto', sans-serif;
                                font-size: 16px;
                                line-height: 22px;
                                padding-top: 0px;
                                padding-right: 0px;
                                padding-bottom: 26px;
                                padding-left: 0;
                              ""
                            >
                              Ch??c b???n c?? m???t ng??y tuy???t v???i, <br />CREL
                            </td>
                          </tr>
                          <tr>
                            <td
                              style=""
                                border-collapse: collapse !important;
                                mso-table-lspace: 0pt;
                                mso-table-rspace: 0pt;
                                padding-left: 0;
                                padding-top: 0;
                                padding-bottom: 0;
                                font-family: 'Roboto', sans-serif;
                                font-size: 14px;
                                line-height: 16px;
                                padding-right: 80px;
                              ""
                            >
                              C???n gi??p ?????? H??y g???i mail t???i
                              <span
                                style=""
                                  color: #4f8df9 !important;
                                  text-decoration: none;
                                ""
                                target=""_blank""
                                >crel2022@gmail.com</span
                              >
                            </td>
                          </tr>
                        </table>
                      </td>
                    </tr>
                  </table>
                </td>
              </tr>
            </table>
          </div>

          <!-- BODY END -->
        </td>
      </tr>
    </table>
  </body>
</html>

";

    public const string htmlContractSignedTemplate = @"
<!DOCTYPE html>
<html>
  <head>
    <style type=""text/css"">
      a .yshortcuts:hover {
        background-color: transparent !important;
        border: none !important;
        color: inherit !important;
      }
      a .yshortcuts:active {
        background-color: transparent !important;
        border: none !important;
        color: inherit !important;
      }
      a .yshortcuts:focus {
        background-color: transparent !important;
        border: none !important;
        color: inherit !important;
      }
    </style>
    <style media=""only screen and (max-width: 520px)"" type=""text/css"">
      /* /\/\/\/\/\/\/\/\/ RESPONSIVE MOJO /\/\/\/\/\/\/\/\/ */
      /*  must escape media query with double symbol */
      @media only screen and (max-width: 520px) {
        .main-table {
          width: 90% !important;
        }
        .top {
          padding-top: 33px !important;
          padding-bottom: 37px !important;
        }
        .content {
          padding: 24px 29px !important;
        }
        .verify-button {
          padding: 25px 0 !important;
        }
      }
    </style>
  </head>
  <body
    align=""center""
    style=""
      margin: 0;
      padding: 0;
      -webkit-text-size-adjust: 100%;
      -ms-text-size-adjust: 100%;
      background: #ffffff;
      width: 100%;
      font-family: 'Roboto', sans-serif;
      font-size: 16px;
      text-align: center;
      line-height: 22px;
      color: #aab2ba;
    ""
    width=""100%""
  >
    <table
      class=""main-table""
      height=""100%""
      style=""
        border-collapse: collapse !important;
        mso-table-lspace: 0pt;
        mso-table-rspace: 0pt;
        font-family: 'Roboto', sans-serif;
        padding-left: 0;
        padding-right: 0;
        padding-top: 0;
        padding-bottom: 0;
        margin: 20px auto 10px;
        padding: 0;
        height: 100%;
        width: 80%;
        max-width: 600px;
      ""
      width=""80%""
    >
      <tr>
        <td
          align=""center""
          class=""top""
          style=""
            border-collapse: collapse !important;
            mso-table-lspace: 0pt;
            mso-table-rspace: 0pt;
            font-family: 'Roboto', sans-serif;
            padding-left: 0;
            padding-right: 0;
            padding-top: 72px;
            padding-bottom: 48px;
          ""
          valign=""top""
        >
          <a
            data-click-track-id=""3182""
            href=""http://crel.site""
            style=""color: #3999c1 !important""
            title=""crelSite""
            ><img
              alt=""Similar Web""
              class=""1ex""
              height=""auto""
              src=""https://i.ibb.co/6vD02sj/crel-full-color-tb.png""
              style=""
                width: 140px;
                line-height: 100%;
                border: 0;
                outline: none;
                text-decoration: none;
              ""
              width=""159""
          /></a>
        </td>
      </tr>
      <tr>
        <td
          align=""center""
          style=""
            border-collapse: collapse !important;
            mso-table-lspace: 0pt;
            mso-table-rspace: 0pt;
            font-family: 'Roboto', sans-serif;
            padding-left: 0;
            padding-right: 0;
            padding-top: 0;
            padding-bottom: 0;
          ""
          valign=""top""
        >
          <!-- BODY -->
          <div
            style=""
              border: 1px solid rgba(223, 226, 230, 0.6);
              border-radius: 4px;
              background-image: url(https://i.ibb.co/wLFPWrc/3891942.jpg);
              background-repeat: no-repeat;
              background-position: bottom -3px right -3px;
              background-size: 36%;
            ""
          >
            <table
              class=""container""
              style=""
                border-collapse: collapse !important;
                mso-table-lspace: 0pt;
                mso-table-rspace: 0pt;
                font-family: 'Roboto', sans-serif;
                padding-left: 0;
                padding-right: 0;
                padding-top: 0;
                padding-bottom: 0;
                width: 100%;
                max-width: 600px;
                margin: 0 auto;
                padding: 0;
                clear: both;
              ""
              width=""100%""
            >
              <tr>
                <td
                  style=""
                    border-collapse: collapse !important;
                    mso-table-lspace: 0pt;
                    mso-table-rspace: 0pt;
                    font-family: 'Roboto', sans-serif;
                    padding-left: 0;
                    padding-right: 0;
                    padding-top: 0;
                    padding-bottom: 0;
                  ""
                >
                  <table
                    class=""row""
                    style=""
                      border-collapse: collapse !important;
                      mso-table-lspace: 0pt;
                      mso-table-rspace: 0pt;
                      font-family: 'Roboto', sans-serif;
                      padding-left: 0;
                      padding-right: 0;
                      padding-top: 0;
                      padding-bottom: 0;
                      width: 100%;
                    ""
                    width=""100%""
                  >
                    <tr>
                      <td
                        class=""content""
                        style=""
                          border-collapse: collapse !important;
                          mso-table-lspace: 0pt;
                          mso-table-rspace: 0pt;
                          font-family: 'Roboto', sans-serif;
                          padding-top: 48px;
                          padding-right: 48px;
                          padding-bottom: 48px;
                          padding-left: 48px;
                        ""
                      >
                        <table
                          class=""row""
                          style=""
                            border-collapse: collapse !important;
                            mso-table-lspace: 0pt;
                            mso-table-rspace: 0pt;
                            font-family: 'Roboto', sans-serif;
                            padding-left: 0;
                            padding-right: 0;
                            padding-top: 0;
                            padding-bottom: 0;
                            width: 100%;
                          ""
                          width=""100%""
                        >
                          <tr>
                            <td
                              style=""
                                border-collapse: collapse !important;
                                mso-table-lspace: 0pt;
                                mso-table-rspace: 0pt;
                                padding-left: 0;
                                padding-right: 0;
                                padding-top: 0;
                                padding-bottom: 0;
                                font-family: 'Roboto', sans-serif;
                                font-size: 24px;
                                line-height: 38px;
                                color: #1b2653;
                              ""
                            >
                              H???p ?????ng m???i t???o m???i th??nh c??ng!
                            </td>
                          </tr>
                          <tr>
                            <td
                              style=""
                                border-collapse: collapse !important;
                                mso-table-lspace: 0pt;
                                mso-table-rspace: 0pt;
                                font-family: 'Roboto', sans-serif;
                                padding-left: 0;
                                padding-right: 0;
                                color: #2a3e52;
                                padding-top: 16px;
                                padding-bottom: 0px;
                              ""
                            >
                              <p>B???n c?? m???t h???p ?????ng m???i ???????c t???o!.</p>
                              <br />
                              <a href=""CrelLink"">
                                <span
                                  style=""color: #00a3b1; text-decoration: none""
                                  >Xem chi ti???t</span
                                >
                              </a>
                              <br />
                              <br />
                              C???m ??n ???? l??m vi???c v???i ch??ng t??i, chi ti???t xin
                              truy c???p trang web:
                              <a href=""https://crel.site"">crel.site</a>.
                            </td>
                          </tr>

                          <tr>
                            <td
                              align=""center""
                              style=""
                                border-collapse: collapse !important;
                                mso-table-lspace: 0pt;
                                mso-table-rspace: 0pt;
                                font-family: 'Roboto', sans-serif;
                                padding-left: 0;
                                padding-right: 0;
                                padding-bottom: 0;
                                color: #000;
                                font-weight: bold;
                                font-size: 24px;
                                padding-top: 35px;
                                padding-bottom: 35px;
                                text-align: center;
                              ""
                            ></td>
                          </tr>
                          <tr>
                            <td
                              style=""
                                border-collapse: collapse !important;
                                mso-table-lspace: 0pt;
                                mso-table-rspace: 0pt;
                                color: #2a3e52;
                                font-family: 'Roboto', sans-serif;
                                font-size: 16px;
                                line-height: 22px;
                                padding-top: 0px;
                                padding-right: 0px;
                                padding-bottom: 26px;
                                padding-left: 0;
                              ""
                            >
                              Ch??c b???n c?? m???t ng??y tuy???t v???i, <br />CREL
                            </td>
                          </tr>
                          <tr>
                            <td
                              style=""
                                border-collapse: collapse !important;
                                mso-table-lspace: 0pt;
                                mso-table-rspace: 0pt;
                                padding-left: 0;
                                padding-top: 0;
                                padding-bottom: 0;
                                font-family: 'Roboto', sans-serif;
                                font-size: 14px;
                                line-height: 16px;
                                padding-right: 80px;
                              ""
                            >
                              C???n gi??p ?????? H??y g???i mail t???i
                              <span
                                style=""
                                  color: #4f8df9 !important;
                                  text-decoration: none;
                                ""
                                target=""_blank""
                                >crel2022@gmail.com</span
                              >
                            </td>
                          </tr>
                        </table>
                      </td>
                    </tr>
                  </table>
                </td>
              </tr>
            </table>
          </div>

          <!-- BODY END -->
        </td>
      </tr>
    </table>
  </body>
</html>

";
}
