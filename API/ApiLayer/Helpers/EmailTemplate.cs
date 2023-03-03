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
                              Xin chào <strong><CrelName></strong>,
                            </td>
                          </tr>
                          <tr>
                            <td style=""border-collapse:collapse !important; mso-table-lspace:0pt; mso-table-rspace:0pt; font-family:'Roboto', sans-serif; padding-left:0; padding-right:0; color:#2A3E52; padding-top:16px; padding-bottom:0px"">Tài khoản của bạn đã được đổi mật khẩu mới. 
                              Sau khi đăng nhập, xin hãy đổi mật khẩu để bảo vệ tài khoản của bạn.</td>
                          </tr>
                          <tr>
                            <td style=""border-collapse:collapse !important; mso-table-lspace:0pt; mso-table-rspace:0pt; font-family:'Roboto', sans-serif; padding-left:0; padding-right:0; color:#2A3E52; padding-top:16px; padding-bottom:0px"">
                              Nếu có vấn đề với việc đăng nhập hãy liên hệ <span style=""color:#4F8DF9 !important; text-decoration:none"" target=""_blank"">crel2022@gmail.com</span>
                            </td>
                          </tr>
                          <tr>
                            <td align=""center"" style=""border-collapse:collapse !important; mso-table-lspace:0pt; mso-table-rspace:0pt; font-family:'Roboto', sans-serif; padding-left:0; padding-right:0; padding-bottom:0; color:#000; font-weight:bold; font-size:24px; padding-top:35px; padding-bottom:35px; text-align:center"">
                              <strong><CrelNewPassword></strong>
                            </td>
                          </tr>
                          <tr>
                            <td style=""border-collapse:collapse !important; mso-table-lspace:0pt; mso-table-rspace:0pt; color:#2A3E52; font-family:'Roboto', sans-serif; font-size:16px; line-height:22px; padding-top:0px; padding-right:0px; padding-bottom:26px; padding-left:0"">
                              Chúc bạn có một ngày tuyệt vời, <br />CREL
                            </td>
                          </tr>
                          <tr>
                            <td style=""border-collapse:collapse !important; mso-table-lspace:0pt; mso-table-rspace:0pt; padding-left:0; padding-top:0; padding-bottom:0; font-family:'Roboto', sans-serif; font-size:14px; line-height:16px; padding-right:80px"">
                              Cần giúp đỡ? Hãy gửi mail tới <span style=""color:#4F8DF9 !important; text-decoration:none"" target=""_blank"">crel2022@gmail.com</span>
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
                              Chào mừng <CrelName> đã đến với CREL!
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
                              <p>Bây giờ bạn đã có thể đăng nhập vào hệ thống CREL với <br/>Tài khoản: <strong><CrelUserName></strong> <br/> Mật khẩu: <strong><CrelPassword></strong></p>
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
                              Chúc bạn có một ngày tuyệt vời, <br />CREL
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
                              Cần giúp đỡ? Hãy gửi mail tới
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
                              Chào <CrelUserName>,
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
                                Chúng tôi nghĩ những mặt bằng bên dưới sẽ phù
                                hợp với bạn
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
                                Cảm ơn đã để lại yêu cầu tìm kiếm trên website
                                của thống tôi, chi tiết đề xuất xin truy cập
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
                              Chúc bạn có một ngày tuyệt vời, <br />CREL
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
                              Cần giúp đỡ? Hãy gửi mail tới
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
                                                                          Được
                                                                          đề
                                                                          xuất
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
                                                          ><CrelPrice>₫</span
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
                              Chào <CrelUserName>,
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
                              Bạn đã tìm kiếm bất động sản với thông tin
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
                                Chúng tôi nghĩ những mặt bằng bên dưới sẽ phù
                                hợp với bạn
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
                              <td style=""padding-left: 15px;"">Quận/Huyện: </td>
                              <td style=""padding-left: 15px;""><CrelArea></td>
                            </tr>
                            <tr>
                              <td style=""padding-left: 15px;"">Khoảng giá: </td>
                              <td style=""padding-left: 15px;""><CrelMinPrice> - <CrelMaxPrice> triệu/tháng</td>
                            </tr>
                            <tr>
                              <td style=""padding-left: 15px;"">Khoảng diện tích: </td>
                              <td style=""padding-left: 15px;""><CrelMinFloorArea> - <CrelMaxFloorArea> m²</td>
                            </tr>
                            <tr>
                              <td style=""padding-left: 15px;"">Mô tả:</td>
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
                                Cảm ơn đã để lại yêu cầu tìm kiếm trên website
                                của thống tôi, chi tiết đề xuất xin truy cập
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
                              Chúc bạn có một ngày tuyệt vời, <br />CREL
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
                              Cần giúp đỡ? Hãy gửi mail tới
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
                              Xin chào <CrelName>,
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
                              Tài khoản <strong><CrelName></strong> đã được xác thực!.<br/>
                              Hiện tại tài khoản <strong><CrelName></strong> đã có thể đăng nhập và sử dụng tất cả dịch vụ
                              tại website: <a href='https://crel.site'>crel.site</a>.
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
                              Chúc bạn có một ngày tuyệt vời, <br />CREL
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
                              Cần giúp đỡ? Hãy gửi mail tới
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
                              Xin chào <CrelName>,
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
                              Tài khoản <strong><CrelName></strong> không được duyệt!.<br/>
                                Lý do: <CrelReason> <br/>
                              
                                  Chúng tôi sẽ sớm liên hệ để tìm hướng xử lý thông qua địa chỉ email hoặc số điện thoại.
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
                              Chúc bạn có một ngày tuyệt vời, <br />CREL
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
                              Cần giúp đỡ? Hãy gửi mail tới
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
                              Cuộc hẹn tạo mới thành công!
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
                              <p>Cuộc hẹn mới đã được tạo!.</p>
                              <br />
                              <p>Thông tin cuộc hẹn:</p>
                              <p>- Thời gian: CrelOnDateTime</p>
                              <p>- Mô tả: CrelDescription</p>
                              <br />
                              <p>Thông tin người môi giới hỗ trợ:</p>
                              <p>- Họ và tên: CrelBrokerName</p>
                              <p>- Số điện thoại: CrelPhoneNumber</p>
                              <br />

                              <a href=""CrelLink"">
                                <span
                                  style=""color: #00a3b1; text-decoration: none""
                                  >Xem chi tiết</span
                                >
                              </a>
                              <br />
                              <br />
                              Cảm ơn đã đặt cuộc hẹn với chúng tôi, chi tiết xin
                              truy cập trang web:
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
                              Chúc bạn có một ngày tuyệt vời, <br />CREL
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
                              Cần giúp đỡ? Hãy gửi mail tới
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
                              Cuộc hẹn tạo mới thất bại,
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
                              Cuộc hẹn bạn tạo vào lúc: <CrelOnDateTime> <br />
                              bị từ chối xét duyệt!.
                              <br />
                              Lý do từ chối cuộc hẹn: <CrelReason>
                              <br />
                              <a href=""CrelLink"">
                                <span
                                  style=""color: #00a3b1; text-decoration: none""
                                  >Xem chi tiết</span
                                >
                              </a>
                              <br />
                              <br />
                              Cảm ơn đã đặt cuộc hẹn với chúng tôi, chi tiết xin
                              truy cập trang web:
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
                              Chúc bạn có một ngày tuyệt vời, <br />CREL
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
                              Cần giúp đỡ? Hãy gửi mail tới
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
                              Hợp đồng mới tạo mới thành công!
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
                              <p>Bạn có một hợp đồng mới được tạo!.</p>
                              <br />
                              <a href=""CrelLink"">
                                <span
                                  style=""color: #00a3b1; text-decoration: none""
                                  >Xem chi tiết</span
                                >
                              </a>
                              <br />
                              <br />
                              Cảm ơn đã làm việc với chúng tôi, chi tiết xin
                              truy cập trang web:
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
                              Chúc bạn có một ngày tuyệt vời, <br />CREL
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
                              Cần giúp đỡ? Hãy gửi mail tới
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
