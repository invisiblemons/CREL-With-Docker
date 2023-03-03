using CREL_BE.Dtos;
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using Syncfusion.Pdf;
using Syncfusion.DocIORenderer;

namespace CREL_BE.Helpers;
public static class ExportHelper
{
    const string contractTemplate = @"
<html>

<body style=""word-wrap:break-word"">

<p style=""text-align:center""><b>CỘNG HÒA XÃ HỘI CHỦ NGHĨA VIỆT NAM</b><br/>
Độc lập - Tự do - Hạnh phúc<br/>
————</p>

<p style=""text-align:right""><CrelSignAddress>………………………………</CrelSignAddress>, ngày <CrelSignDateDay>……</CrelSignDateDay>
tháng <CrelSignDateMonth>……</CrelSignDateMonth> năm <CrelSignDateYear>…………</CrelSignDateYear>.</p>

<p style=""text-align:center""><b>HỢP
ĐỒNG THUÊ NHÀ KINH DOANH</b></p>

<p style=""text-align:center"">Số:
<CrelId>………………………………</CrelId>/<CrelSignDateYear>…………</CrelSignDateYear>/Hợp đồng thuê nhà</p>

<p>Hôm nay, ngày <CrelSignDateDay>……</CrelSignDateDay> tháng <CrelSignDateMonth>……</CrelSignDateMonth> năm <CrelSignDateYear>…………</CrelSignDateYear>, Tại
<CrelSignAddress>………………………………</CrelSignAddress></p>

<p>Chúng tôi gồm có:</p>

<p><b>BÊN CHO THUÊ (BÊN A)</b>: <CrelLessor>………………………………………………………………</CrelLessor><CrelLessor></CrelLessor></p>

<p>Ông/bà: <CrelLessorName>…………………………………………………</CrelLessorName> Sinh ngày: <CrelLessorBirthDate>……………………</CrelLessorBirthDate></p>

<p>CMND/CCCD số: <CrelLessorCccd>…………………</CrelLessorCccd> Ngày cấp: <CrelLessorCccdGrantDate>……………</CrelLessorCccdGrantDate> Nơi cấp: <CrelLessorCccdGrantAddress>…………</CrelLessorCccdGrantAddress></p>

<p>Địa chỉ thường trú: <CrelLessorAddress>…………………………………………………………………………</CrelLessorAddress></p>

<p>Điện thoại: <CrelLessorPhoneNumber>……………………………………………………………………………………</CrelLessorPhoneNumber></p>

<p>Số tài khoản: <CrelLessorBankAccountNumber>……………………………………</CrelLessorBankAccountNumber> mở tại ngân hàng: <CrelLessorBank>…………………</CrelLessorBank></p>

<p>Là chủ sở hữu hợp pháp của căn nhà: <CrelAddress>………………………………………………………………………………………………………………………………………………………………………………………………</CrelAddress></p>

<p><b>BÊN THUÊ (BÊN B):</b> <CrelRenter>………………………………………………………………………</CrelRenter></p>

<p>Địa chỉ trụ sở: <CrelRenterOfficeAddress>………………………………………………………………………………</CrelRenterOfficeAddress></p>

<p>Mã số doanh nghiệp: <CrelRegistrationNumber>……………</CrelRegistrationNumber> cấp ngày: <CrelRegistrationNumberGrantDate>…………</CrelRegistrationNumberGrantDate> nơi cấp: <CrelRegistrationNumberGrantAddress>………………</CrelRegistrationNumberGrantAddress></p>

<p>Ông/bà: <CrelBrandRepresentativeName>………………………</CrelBrandRepresentativeName> là đại diện theo pháp luật sinh ngày: <CrelBrandRepresentativeBirthday>…………</CrelBrandRepresentativeBirthday></p>

<p>CMND/CCCD số: <CrelBrandRepresentativeCccd>………………</CrelBrandRepresentativeCccd> Ngày cấp: <CrelBrandRepresentativeCccdGrantDate>…………</CrelBrandRepresentativeCccdGrantDate> Nơi cấp: <CrelBrandRepresentativeCccdGrantAddress>…………………</CrelBrandRepresentativeCccdGrantAddress></p>

<p>Địa chỉ: <CrelBrandRepresentativeAddress>………………………………………………………………………………………</CrelBrandRepresentativeAddress></p>

<p>Điện thoại: <CrelBrandRepresentativePhoneNumber>……………………………………………………………………………………</CrelBrandRepresentativePhoneNumber></p>

<p>Hai bên cùng thỏa thuận ký hợp đồng thuê nhà kinh doanh với những nội dung sau:</p>

<p><b>ĐIỀU 1. ĐỐI TƯỢNG CỦA HỢP ĐỒNG</b></p>

<p>1.1. Đối tượng của hợp đồng này là ngôi ở địa chỉ: <CrelAddress>………………………………………………………………………………………………………………………………………………………………………………………………</CrelAddress></p>

<p>Diện tích: <CrelArea>………………………………………………………</CrelArea> m2</p>

<p><b>ĐIỀU 2. GIÁ CHO THUÊ NHÀ Ở VÀ PHƯƠNG THỨC THANH TOÁN</b></p>

<p>2.1. Giá cho thuê nhà ở là <CrelPrice>………………</CrelPrice> đồng/tháng (Bằng chữ: <CrelPriceInText>…………………………</CrelPriceInText>), <CrelPaymentTerm></CrelPaymentTerm></p>

<p>Giá cho thuê này đã bao gồm các chi phí về quản lý, bảo trì và vận hành nhà ở.</p>

<p>2.2. Các chi phí sử dụng nước, điện, điện thoại và các dịch vụ khác do bên B thanh toán cho bên cung cấp nước, điện, điện thoại và các cơ quan quản lý dịch vụ.</p>

<p>2.3. Phương thức thanh toán: Tiền mặt hoặc chuyển khoản, trả tiền vào ngày <CrelPayDay>………</CrelPayDay> hàng tháng.</p>

<p><b>ĐIỀU 3. THỜI HẠN THUÊ VÀ THỜI ĐIỂM GIAO NHẬN
NHÀ Ở</b></p>

<p>3.1. Thời hạn thuê ngôi nhà nêu trên là <CrelLeaseLength>……………</CrelLeaseLength> năm. Kể từ
ngày <CrelStartDateDay>…………</CrelStartDateDay> tháng <CrelStartDateMonth>………</CrelStartDateMonth> năm <CrelStartDateYear>………</CrelStartDateYear></p>

<p>3.2. Thời điểm giao nhận nhà là ngày <CrelHandoverDateDay>……</CrelHandoverDateDay> tháng <CrelHandoverDateMonth>……</CrelHandoverDateMonth> năm <CrelHandoverDateYear>…………</CrelHandoverDateYear></p>

<CrelBigTerms></CrelBigTerms>

<table style=""width:100.0%;text-align:center"">
 <tr>
  <td>
  <p><b>Bên thuê</b></p>
  <p>(Ký, ghi rõ họ tên)</p>
  </td>
  <td>
  <p><b>Bên cho thuê</b></p>
  <p>(Ký, ghi rõ họ tên)</p>
  </td>
 </tr>
</table>

<br/>
<br/>
<br/>
<br/>
<br/>
<br/>
<br/>
<br/>
<br/>
<br/>

</body>

</html>

";

    const string contractTemplateWithContractTerm = @"
<html>

<body style=""word-wrap:break-word"">

<p style=""text-align:center""><b>CỘNG HÒA XÃ HỘI CHỦ NGHĨA VIỆT NAM</b><br/>
Độc lập - Tự do - Hạnh phúc<br/>
————</p>

<p style=""text-align:right""><CrelSignAddress>………………………………</CrelSignAddress>, ngày <CrelSignDateDay>……</CrelSignDateDay>
tháng <CrelSignDateMonth>……</CrelSignDateMonth> năm <CrelSignDateYear>…………</CrelSignDateYear>.</p>

<p style=""text-align:center""><b>HỢP
ĐỒNG THUÊ NHÀ KINH DOANH</b></p>

<p style=""text-align:center"">Số:
<CrelId>………………………………</CrelId>/<CrelSignDateYear>…………</CrelSignDateYear>/Hợp đồng thuê nhà</p>

<p>Hôm nay, ngày <CrelSignDateDay>……</CrelSignDateDay> tháng <CrelSignDateMonth>……</CrelSignDateMonth> năm <CrelSignDateYear>…………</CrelSignDateYear>, Tại
<CrelSignAddress>………………………………</CrelSignAddress></p>

<p>Chúng tôi gồm có:</p>

<p><b>BÊN CHO THUÊ (BÊN A)</b>: <CrelLessor>………………………………………………………………</CrelLessor><CrelLessor></CrelLessor></p>

<p>Ông/bà: <CrelLessorName>…………………………………………………</CrelLessorName> Sinh ngày: <CrelLessorBirthDate>……………………</CrelLessorBirthDate></p>

<p>CMND/CCCD số: <CrelLessorCccd>…………………</CrelLessorCccd> Ngày cấp: <CrelLessorCccdGrantDate>……………</CrelLessorCccdGrantDate> Nơi cấp: <CrelLessorCccdGrantAddress>…………</CrelLessorCccdGrantAddress></p>

<p>Địa chỉ thường trú: <CrelLessorAddress>…………………………………………………………………………</CrelLessorAddress></p>

<p>Điện thoại: <CrelLessorPhoneNumber>……………………………………………………………………………………</CrelLessorPhoneNumber></p>

<p>Số tài khoản: <CrelLessorBankAccountNumber>……………………………………</CrelLessorBankAccountNumber> mở tại ngân hàng: <CrelLessorBank>…………………</CrelLessorBank></p>

<p>Là chủ sở hữu hợp pháp của căn nhà: <CrelAddress>………………………………………………………………………………………………………………………………………………………………………………………………</CrelAddress></p>

<p><b>BÊN THUÊ (BÊN B):</b> <CrelRenter>………………………………………………………………………</CrelRenter></p>

<p>Địa chỉ trụ sở: <CrelRenterOfficeAddress>………………………………………………………………………………</CrelRenterOfficeAddress></p>

<p>Mã số doanh nghiệp: <CrelRegistrationNumber>……………</CrelRegistrationNumber> cấp ngày: <CrelRegistrationNumberGrantDate>…………</CrelRegistrationNumberGrantDate> nơi cấp: <CrelRegistrationNumberGrantAddress>………………</CrelRegistrationNumberGrantAddress></p>

<p>Ông/bà: <CrelBrandRepresentativeName>………………………</CrelBrandRepresentativeName> là đại diện theo pháp luật sinh ngày: <CrelBrandRepresentativeBirthday>…………</CrelBrandRepresentativeBirthday></p>

<p>CMND/CCCD số: <CrelBrandRepresentativeCccd>………………</CrelBrandRepresentativeCccd> Ngày cấp: <CrelBrandRepresentativeCccdGrantDate>…………</CrelBrandRepresentativeCccdGrantDate> Nơi cấp: <CrelBrandRepresentativeCccdGrantAddress>…………………</CrelBrandRepresentativeCccdGrantAddress></p>

<p>Địa chỉ: <CrelBrandRepresentativeAddress>………………………………………………………………………………………</CrelBrandRepresentativeAddress></p>

<p>Điện thoại: <CrelBrandRepresentativePhoneNumber>……………………………………………………………………………………</CrelBrandRepresentativePhoneNumber></p>

<p>Hai bên cùng thỏa thuận ký hợp đồng thuê nhà kinh doanh với những nội dung sau:</p>

<p><b>ĐIỀU 1. ĐỐI TƯỢNG CỦA HỢP ĐỒNG</b></p>

<p>1.1. Đối tượng của hợp đồng này là ngôi ở địa chỉ: <CrelAddress>………………………………………………………………………………………………………………………………………………………………………………………………</CrelAddress></p>

<p>Diện tích: <CrelArea>………………………………………………………</CrelArea> m2</p>

<p><b>ĐIỀU 2. GIÁ CHO THUÊ NHÀ Ở VÀ PHƯƠNG THỨC THANH TOÁN</b></p>

<p>2.1. Giá cho thuê nhà ở là <CrelPrice>………………</CrelPrice> đồng/tháng (Bằng chữ: <CrelPriceInText>…………………………</CrelPriceInText>), <CrelPaymentTerm></CrelPaymentTerm></p>

<p>Giá cho thuê này đã bao gồm các chi phí về quản lý, bảo trì và vận hành nhà ở.</p>

<p>2.2. Các chi phí sử dụng nước, điện, điện thoại và các dịch vụ khác do bên B thanh toán cho bên cung cấp nước, điện, điện thoại và các cơ quan quản lý dịch vụ.</p>

<p>2.3. Phương thức thanh toán: Tiền mặt hoặc chuyển khoản, trả tiền vào ngày <CrelPayDay>………</CrelPayDay> hàng tháng.</p>

<p><b>ĐIỀU 3. THỜI HẠN THUÊ VÀ THỜI ĐIỂM GIAO NHẬN
NHÀ Ở</b></p>

<p>3.1. Thời hạn thuê ngôi nhà nêu trên là <CrelLeaseLength>……………</CrelLeaseLength> năm. Kể từ
ngày <CrelStartDateDay>…………</CrelStartDateDay> tháng <CrelStartDateMonth>………</CrelStartDateMonth> năm <CrelStartDateYear>………</CrelStartDateYear></p>

<p>3.2. Thời điểm giao nhận nhà là ngày <CrelHandoverDateDay>……</CrelHandoverDateDay> tháng <CrelHandoverDateMonth>……</CrelHandoverDateMonth> năm <CrelHandoverDateYear>…………</CrelHandoverDateYear></p>

<p><b>ĐIỀU 4. NGHĨA VỤ VÀ QUYỀN CỦA BÊN A</b></p>

<p>4.1. Nghĩa vụ của bên A:</p>

<p>Giao nhà ở và trang thiết bị gắn liền với nhà ở (nếu có) cho bên B theo đúng hợp đồng;</p>

<p>Tạo điều kiện cho bên B sử dụng thuận tiện diện tích thuê;</p>

<p>Bảo dưỡng, sửa chữa nhà theo định kỳ hoặc theo thỏa thuận; nếu bên A không bảo dưỡng, sửa chữa nhà mà gây thiệt hại cho bên B, thì phải bồi thường;</p>

<p>Nộp các khoản thuế về nhà và đất (nếu có);</p>

<p>Xuất hoá đơn giá trị gia tăng theo yêu cầu của bên thuê (nếu có);</p>

<p>Bảo đảm cho bên B sử dụng ổn định nhà trong thời hạn thuê;</p>

<p>4.2. Quyền của bên A:</p>

<p>Đơn phương chấm dứt thực hiện hợp đồng thuê nhà kinh doanh nhưng phải báo cho bên B biết trước ít nhất 30 ngày nếu không có thỏa thuận khác và yêu cầu bồi thường thiệt hại nếu bên B;</p>

<p>Yêu cầu bên B trả đủ tiền thuê nhà đúng kỳ hạn như đã thỏa thuận;</p>

<p>Sử dụng nhà không đúng mục đích thuê như đã thỏa thuận trong hợp đồng;</p>

<p>Không trả tiền thuê nhà liên tiếp trong ba tháng trở lên mà không có lý do chính đáng;</p>

<p>Bên B chuyển đổi, cho mượn, cho thuê lại nhà ở đang thuê mà không có sự đồng ý của bên A;</p>

<p>Bên B tự ý đục phá, cơi nới, cải tạo, phá dỡ nhà ở đang thuê;</p>

<p>Yêu cầu bên B có trách nhiệm trong việc sửa chữa phần hư hỏng, bồi thường thiệt hại do lỗi của bên B gây ra khi kết thúc hợp đồng.</p>

<p><b>ĐIỀU 5. NGHĨA VỤ VÀ QUYỀN CỦA BÊN B</b></p>

<p>5.1. Nghĩa vụ của bên B:</p>

<p>Trả tiền điện, nước, điện thoại, vệ sinh và các chi phí phát sinh khác trong thời gian thuê nhà;</p>

<p>Giao lại nhà cho bên A trong các trường hợp chấm dứt hợp đồng quy định tại hợp đồng thuê nhà kinh doanh này;</p>

<p>Sử dụng nhà đúng mục đích đã thỏa thuận, giữ gìn nhà ở và có trách nhiệm trong việc sửa chữa những hư hỏng do mình gây ra;</p>

<p>Trả đủ tiền thuê nhà đúng kỳ hạn đã thỏa thuận;</p>

<p>Không được chuyển nhượng hợp đồng thuê nhà hoặc cho người khác thuê lại trừ trường hợp được bên A đồng ý bằng văn bản;</p>

<p>Chấp hành các quy định về giữ gìn vệ sinh môi trường và an ninh trật tự trong khu vực cư trú;</p>

<p>Chấp hành đầy đủ những quy định về quản lý sử dụng;</p>

<p>Trả nhà cho bên A theo đúng thỏa thuận.</p>

<p>5.2. Quyền của bên B:</p>

<p>Được cho thuê lại nhà đang thuê, nếu được bên cho thuê đồng ý bằng văn bản;</p>

<p>Được tiếp tục thuê theo các điều kiện đã thỏa thuận với bên A trong trường hợp thay đổi chủ sở hữu nhà;</p>

<p>Nhận nhà ở và trang thiết bị gắn liền (nếu có) theo đúng thoả thuận;</p>

<p>Không sửa chữa nhà ở khi có hư hỏng nặng mặc dù bên B đã yêu cầu bằng văn bản;</p>

<p>Được ưu tiên ký hợp đồng thuê nhà kinh doanh tiếp, nếu đã hết hạn thuê mà nhà vẫn dùng để cho thuê;</p>

<p>Quyền sử dụng nhà ở bị hạn chế do lợi ích của người thứ ba;</p>

<p>Yêu cầu bên A sửa chữa nhà đang cho thuê trong trường hợp nhà bị hư hỏng nặng;</p>

<p>Tăng giá thuê nhà ở bất hợp lý hoặc tăng giá thuê mà không thông báo cho bên thuê nhà ở biết trước theo thỏa thuận.</p>

<p><b>ĐIỀU 6. QUYỀN TIẾP TỤC THUÊ NHÀ</b></p>

<p>Trường hợp chủ sở hữu nhà ở chết mà thời hạn thuê nhà ở vẫn còn thì bên B được tiếp tục thuê đến hết hạn hợp đồng thuê nhà kinh doanh. Người thừa kế có trách nhiệm tiếp tục thực hiện hợp đồng thuê nhà ở đã ký kết trước đó. Trừ trường hợp các bên có thỏa thuận khác.</p>

<p>Trường hợp chủ sở hữu không có người thừa kế hợp pháp theo quy định pháp luật thì nhà ở đó thuộc quyền sở hữu của Nhà nước và người đang thuê nhà ở sẽ tiếp tục được thuê theo quy định về quản lý, sử dụng nhà ở thuộc sở hữu nhà nước.</p>

<p>Trường hợp chủ sở hữu nhà ở chuyển quyền sở hữu nhà ở đang cho thuê cho người khác mà thời hạn thuê nhà ở vẫn còn thì bên B vẫn tiếp tục thuê đến hết hạn hợp đồng; chủ sở hữu nhà ở mới có trách nhiệm tiếp tục thực hiện hợp đồng thuê nhà ở đã ký kết trước đó. Trừ trường hợp các bên có thỏa thuận khác.</p>

<p><b>ĐIỀU 7. TRÁCH NHIỆM DO VI PHẠM HỢP ĐỒNG</b></p>

<p>Trong quá trình thực hiện hợp đồng thuê nhà kinh doanh mà có phát sinh tranh chấp, các bên cùng nhau thương lượng giải quyết. Trong trường hợp không tự giải quyết được, phải thực hiện bằng cách hòa giải. Nếu hòa giải không thành thì đưa ra Tòa án có thẩm quyền theo quy định của pháp luật.</p>

<p><b>ĐIỀU 8. CÁC THỎA THUẬN KHÁC</b></p>

<p>8.1. Việc sửa đổi, bổ sung hoặc hủy bỏ hợp đồng này phải lập thành văn bản và có chữ ký của hai bên.</p>

<p>8.2. Hợp đồng thuê nhà này sẽ chỉ chấm dứt trong những trường hợp sau:</p>

<p>Khi hết thời hạn mà không có thoả thuận gia hạn hợp đồng thuê theo quy định tại Điều 3.1 hợp đồng này;</p>

<p>Tài sản thuê bị phá huỷ và hoàn toàn không thể sử dụng được;</p>

<p>Trong trường hợp Bên Thuê vi phạm hợp đồng theo khoản c điều 4.2 hợp đồng thuê nhà kinh doanh này;</p>

<p>Bên thuê bị phá sản;</p>

<p>Trong trường hợp bất khả kháng theo quy định của pháp luật.</p>

<p><b>ĐIỀU 9. CAM KẾT CỦA CÁC BÊN</b></p>

<p>Bên A và bên B chịu trách nhiệm trước pháp luật về những lời cùng cam kết sau đây:</p>

<p>1. Đã khai đúng sự thật và tự chịu trách nhiệm về tính chính xác của các thông tin về nhân thân đã ghi trong hợp đồng này.</p>

<p>2. Thực hiện đúng và đầy đủ tất cả các thỏa thuận đã ghi trong hợp đồng này; nếu bên nào vi phạm mà gây thiệt hại, thì phải bồi thường cho bên kia hoặc cho người thứ ba (nếu có).</p>

<p>3. Trong quá trình thực hiện nếu phát hiện thấy những vấn đề cần thoả thuận thì hai bên có thể lập thêm Phụ lục hợp đồng. Nội dung Phụ lục Hợp đồng có giá trị pháp lý như hợp đồng chính.</p>

<p>4. Hợp đồng này có giá trị kể từ ngày hai bên ký kết (trường hợp là cá nhân cho thuê nhà ở từ 06 tháng trở lên thì Hợp đồng có hiệu lực kể từ ngày ""Hợp đồng thuê nhà kinh doanh"" được công chứng hoặc chứng thực).</p>

<p><b>ĐIỀU 10. ĐIỀU KHOẢN CUỐI CÙNG</b></p>

<p>1. Hai bên đã hiểu rõ quyền, nghĩa vụ và lợi ích hợp pháp của mình, ý nghĩa và hậu quả pháp lý của việc công chứng (chứng thực) này, sau khi đã được nghe lời giải thích của người có thẩm quyền công chứng hoặc chứng thực dưới đây.</p>

<p>2. Hai bên đã đọc, đã hiểu và đồng ý tất cả các điều khoản ghi trong hợp đồng này.</p>

<p>Hợp đồng thuê nhà kinh doanh được lập thành ………. (………..) bản, mỗi bên giữ một bản và có giá trị như nhau.</p>

<table style=""width:100.0%;text-align:center"">
 <tr>
  <td>
  <p><b>Bên thuê</b></p>
  <p>(Ký, ghi rõ họ tên)</p>
  </td>
  <td>
  <p><b>Bên cho thuê</b></p>
  <p>(Ký, ghi rõ họ tên)</p>
  </td>
 </tr>
</table>

<br/>
<br/>
<br/>
<br/>
<br/>
<br/>
<br/>
<br/>
<br/>
<br/>

</body>

</html>

";

    private static string ReplaceWithTag(this string inString, string tag, string? toString)
    {
        if (toString == null)
        {
            return inString;
        }
        string beginTag = "<" + tag + ">";
        string endTag = "</" + tag + ">";
        var start = inString.IndexOf(beginTag);
        var end = inString.IndexOf(endTag) + endTag.Length;

        while (start != -1 && end != -1 && start < end)
        {
            inString = inString.Replace(
                inString.Substring(start, end - start),
                toString
            );
            start = inString.IndexOf(beginTag);
            end = inString.IndexOf(endTag) + endTag.Length;
        }
        return inString;
    }

    private static string? ToDateString(this DateTime? date)
    {
        if (date == null)
        {
            return null;
        }
        else
        {
            return date.Value.Day + "/" + date.Value.Month + "/" + date.Value.Year;
        }
    }

    private static string? BigTermsToHtmlTag(ICollection<CreateFirstLevelContractTermDto>? bigTerms)
    {
        if (bigTerms == null)
        {
            return null;
        }
        int bigTermCount = 3;
        int smallTermCount = 0;
        string response = "";
        foreach (var bigTerm in bigTerms)
        {
            if (bigTerm != null)
            {
                bigTermCount++;
                smallTermCount = 0;

                if (bigTerm.Title != null)
                {
                    response += "<p><b>" + bigTerm.Title.ToUpper() + "</b></p>";
                }
                if (bigTerm.Content != null)
                {
                    response += "<p>" + bigTerm.Content + "</p>";
                }
                if (bigTerm.ContractTerms != null)
                {
                    foreach (var smallTerm in bigTerm.ContractTerms)
                    {
                        if (smallTerm != null)
                        {
                            smallTermCount++;
                            if (smallTerm.Title != null)
                            {
                                response += "<p>" + smallTerm.Title + "</p>";
                            }
                            if (smallTerm.Content != null)
                            {
                                response += "<p>" + smallTerm.Content + "</p>";
                            }
                        }
                    }
                }
            }
        }
        return response;
    }

    public static async Task<string> ExportContractToWordAsync(ContractInformationForExport contract)
    {
        string useContractTemplate = contractTemplate;
        if (contract.ContractTerms == null || !contract.ContractTerms.Any())
        {
            useContractTemplate = contractTemplateWithContractTerm;
        }
        var htmlPath = Path.GetTempFileName();
        htmlPath = Path.ChangeExtension(htmlPath, ".html");
        await File.WriteAllTextAsync(htmlPath,
            useContractTemplate
            .ReplaceWithTag("CrelLessor", contract.Lessor)
            .ReplaceWithTag("CrelLessorName", contract.LessorName)
            .ReplaceWithTag("CrelLessorBirthDate", contract.LessorBirthDate.ToDateString())
            .ReplaceWithTag("CrelLessorCccd", contract.LessorCccd)
            .ReplaceWithTag("CrelLessorCccdGrantDate", contract.LessorCccdGrantDate.ToDateString())
            //5
            .ReplaceWithTag("CrelLessorCccdGrantAddress", contract.LessorCccdGrantAddress)
            .ReplaceWithTag("CrelLessorAddress", contract.LessorAddress)
            .ReplaceWithTag("CrelLessorPhoneNumber", contract.LessorPhoneNumber)
            .ReplaceWithTag("CrelLessorBankAccountNumber", contract.LessorBankAccountNumber)
            .ReplaceWithTag("CrelLessorBank", contract.LessorBank)
            //10
            .ReplaceWithTag("CrelAddress", contract.Address)
            .ReplaceWithTag("CrelRenter", contract.Renter)
            .ReplaceWithTag("CrelRenterOfficeAddress", contract.RenterOfficeAddress)
            .ReplaceWithTag("CrelRegistrationNumber", contract.RegistrationNumber)
            .ReplaceWithTag("CrelRegistrationNumberGrantDate", contract.RegistrationNumberGrantDate.ToDateString())
            //15
            .ReplaceWithTag("CrelRegistrationNumberGrantAddress", contract.RegistrationNumberGrantAddress)
            .ReplaceWithTag("CrelBrandRepresentativeName", contract.BrandRepresentativeName)
            .ReplaceWithTag("CrelBrandRepresentativeBirthday", contract.BrandRepresentativeBirthday.ToDateString())
            .ReplaceWithTag("CrelBrandRepresentativeCccd", contract.BrandRepresentativeCccd)
            .ReplaceWithTag("CrelBrandRepresentativeCccdGrantDate", contract.BrandRepresentativeCccdGrantDate.ToDateString())
            //20
            .ReplaceWithTag("CrelBrandRepresentativeCccdGrantAddress", contract.BrandRepresentativeCccdGrantAddress)
            .ReplaceWithTag("CrelBrandRepresentativeAddress", contract.BrandRepresentativeAddress)
            .ReplaceWithTag("CrelBrandRepresentativePhoneNumber", contract.BrandRepresentativePhoneNumber)
            //trùng address
            .ReplaceWithTag("CrelArea", contract.Area.ToString())
            //25
            .ReplaceWithTag("CrelPrice", (contract.Price ?? 0).ToString("### ### ### ###").Trim().Replace(" ","."))
            .ReplaceWithTag("CrelPriceInText", contract.PriceInText)
            .ReplaceWithTag("CrelPayDay", contract.PayDay.ToString())
            .ReplaceWithTag("CrelPaymentTerm", contract.PaymentTerm)
            .ReplaceWithTag("CrelLeaseLength", contract.LeaseLength.ToString())
            //30
            .ReplaceWithTag("CrelStartDateDay", contract.StartDate == null ? null : contract.StartDate.Value.Day.ToString())
            .ReplaceWithTag("CrelStartDateMonth", contract.StartDate == null ? null : contract.StartDate.Value.Month.ToString())
            .ReplaceWithTag("CrelStartDateYear", contract.StartDate == null ? null : contract.StartDate.Value.Year.ToString())
            .ReplaceWithTag("CrelHandoverDateDay", contract.HandoverDate == null ? null : contract.HandoverDate.Value.Day.ToString())
            .ReplaceWithTag("CrelHandoverDateMonth", contract.HandoverDate == null ? null : contract.HandoverDate.Value.Month.ToString())
            //35
            .ReplaceWithTag("CrelHandoverDateYear", contract.HandoverDate == null ? null : contract.HandoverDate.Value.Year.ToString())
            .ReplaceWithTag("CrelBigTerms", BigTermsToHtmlTag(contract.ContractTerms))
            .ReplaceWithTag("CrelSignAddress", contract.SignAddress)
            .ReplaceWithTag("CrelSignDateDay", contract.SignDate  == null ? null : contract.SignDate.Value.Day.ToString())
            .ReplaceWithTag("CrelSignDateMonth", contract.SignDate  == null ? null : contract.SignDate.Value.Month.ToString())
            //40
            .ReplaceWithTag("CrelSignDateYear", contract.SignDate  == null ? null : contract.SignDate.Value.Year.ToString())
            .ReplaceWithTag("CrelId", (contract.Id ?? 0).ToString("#"))
        );
        var doc = new WordDocument(System.IO.File.OpenRead(htmlPath), FormatType.Html, XHTMLValidationType.None);
        var wordPath = Path.GetTempFileName();
        wordPath = Path.ChangeExtension(wordPath, ".doc");
        var writeStream = System.IO.File.OpenWrite(wordPath);
        doc.Save(writeStream, FormatType.Doc);
        doc.Close();
        writeStream.Close();
        return wordPath;
    }

    public static async Task<string> ExportContractToPdfAsync(ContractInformationForExport contract)
    {
        string useContractTemplate = contractTemplate;
        if (contract.ContractTerms == null || !contract.ContractTerms.Any())
        {
            useContractTemplate = contractTemplateWithContractTerm;
        }
        var htmlPath = Path.GetTempFileName();
        htmlPath = Path.ChangeExtension(htmlPath, ".html");
        await File.WriteAllTextAsync(htmlPath,
            useContractTemplate
            .ReplaceWithTag("CrelLessor", contract.Lessor)
            .ReplaceWithTag("CrelLessorName", contract.LessorName)
            .ReplaceWithTag("CrelLessorBirthDate", contract.LessorBirthDate.ToDateString())
            .ReplaceWithTag("CrelLessorCccd", contract.LessorCccd)
            .ReplaceWithTag("CrelLessorCccdGrantDate", contract.LessorCccdGrantDate.ToDateString())
            //5
            .ReplaceWithTag("CrelLessorCccdGrantAddress", contract.LessorCccdGrantAddress)
            .ReplaceWithTag("CrelLessorAddress", contract.LessorAddress)
            .ReplaceWithTag("CrelLessorPhoneNumber", contract.LessorPhoneNumber)
            .ReplaceWithTag("CrelLessorBankAccountNumber", contract.LessorBankAccountNumber)
            .ReplaceWithTag("CrelLessorBank", contract.LessorBank)
            //10
            .ReplaceWithTag("CrelAddress", contract.Address)
            .ReplaceWithTag("CrelRenter", contract.Renter)
            .ReplaceWithTag("CrelRenterOfficeAddress", contract.RenterOfficeAddress)
            .ReplaceWithTag("CrelRegistrationNumber", contract.RegistrationNumber)
            .ReplaceWithTag("CrelRegistrationNumberGrantDate", contract.RegistrationNumberGrantDate.ToDateString())
            //15
            .ReplaceWithTag("CrelRegistrationNumberGrantAddress", contract.RegistrationNumberGrantAddress)
            .ReplaceWithTag("CrelBrandRepresentativeName", contract.BrandRepresentativeName)
            .ReplaceWithTag("CrelBrandRepresentativeBirthday", contract.BrandRepresentativeBirthday.ToDateString())
            .ReplaceWithTag("CrelBrandRepresentativeCccd", contract.BrandRepresentativeCccd)
            .ReplaceWithTag("CrelBrandRepresentativeCccdGrantDate", contract.BrandRepresentativeCccdGrantDate.ToDateString())
            //20
            .ReplaceWithTag("CrelBrandRepresentativeCccdGrantAddress", contract.BrandRepresentativeCccdGrantAddress)
            .ReplaceWithTag("CrelBrandRepresentativeAddress", contract.BrandRepresentativeAddress)
            .ReplaceWithTag("CrelBrandRepresentativePhoneNumber", contract.BrandRepresentativePhoneNumber)
            //trùng address
            .ReplaceWithTag("CrelArea", contract.Area.ToString())
            //25
            .ReplaceWithTag("CrelPrice", (contract.Price ?? 0).ToString("### ### ### ###").Trim().Replace(" ","."))
            .ReplaceWithTag("CrelPriceInText", contract.PriceInText)
            .ReplaceWithTag("CrelPayDay", contract.PayDay.ToString())
            .ReplaceWithTag("CrelPaymentTerm", contract.PaymentTerm)
            .ReplaceWithTag("CrelLeaseLength", contract.LeaseLength.ToString())
            //30
            .ReplaceWithTag("CrelStartDateDay", contract.StartDate == null ? null : contract.StartDate.Value.Day.ToString())
            .ReplaceWithTag("CrelStartDateMonth", contract.StartDate == null ? null : contract.StartDate.Value.Month.ToString())
            .ReplaceWithTag("CrelStartDateYear", contract.StartDate == null ? null : contract.StartDate.Value.Year.ToString())
            .ReplaceWithTag("CrelHandoverDateDay", contract.HandoverDate == null ? null : contract.HandoverDate.Value.Day.ToString())
            .ReplaceWithTag("CrelHandoverDateMonth", contract.HandoverDate == null ? null : contract.HandoverDate.Value.Month.ToString())
            //35
            .ReplaceWithTag("CrelHandoverDateYear", contract.HandoverDate == null ? null : contract.HandoverDate.Value.Year.ToString())
            .ReplaceWithTag("CrelBigTerms", BigTermsToHtmlTag(contract.ContractTerms))
            .ReplaceWithTag("CrelSignAddress", contract.SignAddress)
            .ReplaceWithTag("CrelSignDateDay", contract.SignDate  == null ? null : contract.SignDate.Value.Day.ToString())
            .ReplaceWithTag("CrelSignDateMonth", contract.SignDate  == null ? null : contract.SignDate.Value.Month.ToString())
            //40
            .ReplaceWithTag("CrelSignDateYear", contract.SignDate  == null ? null : contract.SignDate.Value.Year.ToString())
            .ReplaceWithTag("CrelId", (contract.Id ?? 0).ToString("#"))
        );
        var doc = new WordDocument(System.IO.File.OpenRead(htmlPath), FormatType.Html, XHTMLValidationType.None);
        var wordPath = Path.GetTempFileName();
        wordPath = Path.ChangeExtension(wordPath, ".doc");
        var docWriteStream = System.IO.File.OpenWrite(wordPath);
        doc.Save(docWriteStream, FormatType.Doc);

        DocIORenderer render = new DocIORenderer();
        //Sets Chart rendering Options.
        render.Settings.ChartRenderingOptions.ImageFormat = Syncfusion.OfficeChart.ExportImageFormat.Png;
        //Converts Word document into PDF document
        PdfDocument pdf = render.ConvertToPDF(doc);
        //Releases all resources used by the Word document and DocIO Renderer objects
        render.Dispose();
        //Saves the PDF file
        var pdfPath = Path.GetTempFileName();
        pdfPath = Path.ChangeExtension(pdfPath, ".pdf");
        var pdfWriteStream = System.IO.File.OpenWrite(pdfPath);
        pdf.Save(pdfWriteStream);
        //Closes the instance of PDF document object

        pdf.Close();

        doc.Close();

        docWriteStream.Close();
        pdfWriteStream.Close();
        return pdfPath;
    }

    // public static async Task<string> ExportContractToWordAsync(ContractInformationForExport contract)
    // {
    //     var htmlPath = Path.GetTempFileName();
    //     htmlPath = Path.ChangeExtension(htmlPath, ".html");
    //     await File.WriteAllTextAsync(htmlPath,text
    //     );
    //     var doc = new Document(htmlPath);
    //     var wordPath = Path.GetTempFileName();
    //     wordPath = Path.ChangeExtension(wordPath, ".doc");
    //     doc.Save(wordPath);
    //     return wordPath;
    // }

    // public static async Task<string> ExportContractToWordAsync(ContractInformationForExport contract)
    // {
    //     var htmlPath = Path.GetTempFileName();
    //     htmlPath = Path.ChangeExtension(htmlPath, ".html");
    //     await File.WriteAllTextAsync(htmlPath, contractTemplateWithContractTerm);
    //     var doc = new WordDocument(System.IO.File.OpenRead(htmlPath), FormatType.Html, XHTMLValidationType.None);
    //     var wordPath = Path.GetTempFileName();
    //     wordPath = Path.ChangeExtension(wordPath, ".doc");
    //     var writeStream = System.IO.File.OpenWrite(wordPath);
    //     doc.Save(writeStream, FormatType.Doc);
    //     doc.Close();
    //     writeStream.Close();
    //     return wordPath;
    // }
}