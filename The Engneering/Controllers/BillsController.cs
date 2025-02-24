using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Drawing.Printing;
using The_Engneering.Contracts.Bill;
using The_Engneering.Contracts.Product;
using The_Engneering.Services;

namespace The_Engneering.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BillsController(IBillService _billService, ApplicationDbContext _context) : ControllerBase
{
    private readonly IBillService billService = _billService;
    private readonly ApplicationDbContext context = _context;

    [HttpGet("")]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts(CancellationToken cancellationToken)
    {
        var polls = await billService.GetAllAsync(cancellationToken);
        return Ok(polls.Adapt<IEnumerable<BillResponse>>());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct([FromRoute]string id, CancellationToken cancellationToken)
    {
        var bill = await billService.GetAsync(id, cancellationToken);
        if (bill == null)
            return NotFound();
        var billResponse = bill.Adapt<BillResponse>();
        return Ok(billResponse);
    }

    [HttpPost("{id}")]
    public async Task<IActionResult> AddBill([FromRoute] int id, [FromBody] BillRequest request, CancellationToken cancellationToken)
    {
        var bill = await billService.AddAsync(id, request.Adapt<Bill>(), cancellationToken);
        return Created();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] string id,
                                            [FromBody] BillRequest request,
                                            CancellationToken cancellationToken)
    {
        var isUpdated = await billService.UpdateAsync(id, request.Adapt<Bill>(), cancellationToken);

        if (!isUpdated)
            return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] string id, CancellationToken cancellationToken)
    {
        var isDeleted = await billService.DeleteAsync(id, cancellationToken);

        if (!isDeleted)
            return NotFound();
        return NoContent();
    }

    [HttpGet("{id}/print")]
    public async Task<IActionResult> PrintBill(string id, CancellationToken cancellationToken = default)
    {
        var bill = await context.Bills
            .AsNoTracking()
            .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);

        if (bill == null)
            return NotFound("Bill not found");

        // Start printing
        PrintDocument printDoc = new PrintDocument();
        printDoc.PrintPage += (sender, e) => PrintPage(e, bill);
        printDoc.Print();
        printDoc.Print();

        return Ok("Bill sent to printer successfully.");
    }

    private void PrintPage(PrintPageEventArgs e, Bill bill)
    {   
        Font titleFont = new Font("Arial", 18, FontStyle.Bold);
        Font headerFont = new Font("Arial", 14, FontStyle.Bold);
        Font contentFont = new Font("Arial", 12);
        float pageWidth = e.PageBounds.Width;
        float margin = 100;
        float yPos = 100;

        // رسم خط علوي
        e.Graphics.DrawLine(Pens.Black, margin, yPos, pageWidth - margin, yPos);
        yPos += 20;

        // طباعة العنوان الرئيسي في المنتصف
        StringFormat centerAlign = new StringFormat { Alignment = StringAlignment.Center };
        e.Graphics.DrawString("الـــــمـــــهــــــنــــــدس", titleFont, Brushes.Black, new PointF(pageWidth / 2, yPos), centerAlign);
        yPos += 40;

        // رسم خط أسفل العنوان
        e.Graphics.DrawLine(Pens.Black, margin, yPos, pageWidth - margin, yPos);
        yPos += 20;

        // إعداد المحاذاة لليمين
        StringFormat rightAlign = new StringFormat { Alignment = StringAlignment.Far };
        float labelX = pageWidth - margin; // موضع النص
        float colonX = labelX - 15; // موضع النقطتين
        float valueX = colonX - 200; // موضع القيم

        // بيانات الفاتورة
        (string Label, string Value)[] billData =
        {
                ("رقم الفاتورة", bill.Id),
                ("اسم العميل", bill.CustomerName),
                ("رقم التلفون ", bill.CustomerPhone),
                ("اسم المنتج", bill.ProductName),
                ("الكميه", bill.Amount.ToString()),
                ("طريقة الدفع", bill.PayType),
                ("الخصم", $"{bill.Discount}"),
                ("سعر المنتج", $"{bill.Price}"),
                ("السعر الاجمالي", $"{bill.TotalPrice}"),
                ("تاريخ الفاتورة", bill.CreatedOn.ToString("yyyy-MM-dd"))
        };

        foreach (var item in billData)
        {
            e.Graphics.DrawString(item.Label, headerFont, Brushes.Black, new PointF(labelX, yPos), rightAlign);
            e.Graphics.DrawString(":", headerFont, Brushes.Black, new PointF(colonX, yPos), rightAlign);
            e.Graphics.DrawString(item.Value, contentFont, Brushes.Black, new PointF(valueX, yPos));
            yPos += 30;
        }

        // رسم خط قبل رسالة الشكر
        yPos += 20;
        e.Graphics.DrawLine(Pens.Black, margin, yPos, pageWidth - margin, yPos);
        yPos += 30;

        // طباعة رسالة الشكر في المنتصف
        e.Graphics.DrawString("شكراً لثقتكم بنا", new Font("Arial", 14, FontStyle.Italic), Brushes.Black, new PointF(pageWidth / 2, yPos), centerAlign);
    }
}

