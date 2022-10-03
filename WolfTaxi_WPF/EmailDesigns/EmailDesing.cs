using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WolfTaxi_WPF.EmailDesigns
{
    public abstract class EmailDesing
    {
        public static string SecurityCode(string code)
        {
            StringBuilder sb = new StringBuilder(@"<html>
  <head>
    <style>
      @import url(""https://fonts.googleapis.com/css?family=Open+Sans"");

      * {
        box-sizing: border-box;
      }

      body {
        background-color: #fafafa;
        display: flex;
        justify-content: center;
        align-items: center;
      }
      .c-email {
        width: 470px;
        border-radius: 40px;
        height: 200px;
        overflow: hidden;
        box-shadow: 0px 7px 22px 0px rgba(0, 0, 0, 0.1);
      }


      .c-email__header {
        background-color: #0fd59f;
        width: 100%;
        height: 60px;
      }
      .c-email__header__title {
        font-size: 23px;
        font-family: ""Open Sans"";
        height: 60px;
        line-height: 60px;
        margin: 0;
        text-align: center;
        color: white;
      }
      .c-email__content {
        width: 470px;
        height: 200px;
        display: flex;
        flex-direction: row;
        justify-content: center;
        align-items: flex-end;
        align-self: flex-end;
        flex-wrap: wrap;
        background-color: #f6f8fc;
        padding: 15px;
      }
      .c-email__content__text {
        font-size: 20px;
        width: 150px;
        height: 60px ;
        margin-top: 30px;
        text-align: center;
        display: flex;
        flex-direction: row;
        align-self: flex-start;
        text-align: center;
        color: #343434;
      }
      .c-email__code {
        width: 200px;
        height: 100px;
        margin-left: 70px;
        background-color: #ddd;
        border-radius: 40px;
        padding: 20px;
        text-align: center;
        font-size: 36px;
        font-family: ""Open Sans"";
        letter-spacing: 10px;
        box-shadow: 0px 7px 22px 0px rgba(0, 0, 0, 0.1);
      }
      .c-email__footer {
        width: 100%;
        height: 60px;
        background-color: #f6f8fc;
      }
      .text-title {
        font-family: ""Open Sans"";
      }
      .text-center {
        text-align: center;
      }
      .text-italic {
        font-style: italic;
      }
      .opacity-30 {
        opacity: 0.3;
      }
      .mb-0 {
        margin-bottom: 0;
      }
      
    </style>
  </head>
  <body>

  <div class=""c-email"">
    <div class=""c-email__header"">
      <h1 class=""c-email__header__title"">Your Security Code</h1>
    </div>
    <div class=""c-email__content"">
      <p class=""c-email__content__text text-title"">Security code:</p>
      <div class=""c-email__code"">
        <span class=""c-email__code__text"">");

            sb.Append(code);

            sb.Append(@"</span>
      </div>
  </div>
  <div class=""c-email__footer""></div>
</div>


  </body>
</html>");

            return sb.ToString();

            ;

        }

    }
}
