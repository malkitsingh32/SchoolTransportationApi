using Application.Abstraction.Services;
using Application.Common.Helpers;
using DTO.Response.Students;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Threading.Tasks;

namespace Infrastructure.Implementation.Services
{
    /// <summary>
    /// Generates the Washingtonville Central School District
    /// Application for Transportation as a single-page PDF using
    /// absolute canvas positioning (PdfContentByte) for pixel-perfect layout.
    /// </summary>
    public class PdfBuilderService : IPdfBuilderService
    {
        // ── Page geometry ────────────────────────────────────────────────
        private const float PW = 612f;   // Letter width  (pts)
        private const float PH = 792f;   // Letter height (pts)
        private const float ML = 45f;    // Margin left
        private const float MR = 45f;    // Margin right
        private const float MT = 36f;    // Margin top
        private const float CW = PW - ML - MR;   // 522 pt content width
        private const float LX = ML + 9f;         // Left x inside boxes
        private const float RX = ML + CW - 9f;    // Right x inside boxes
        private const float PAD = 9f;

        // ── Colours ──────────────────────────────────────────────────────
        private static readonly BaseColor CBlack = new BaseColor(0, 0, 0);
        private static readonly BaseColor CRed = new BaseColor(204, 0, 0);
        private static readonly BaseColor CWhite = BaseColor.WHITE;

        // ── Font helpers (iTextSharp BaseFont for canvas drawing) ────────
        private static readonly BaseFont BF_REG = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, false);
        private static readonly BaseFont BF_BOLD = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, false);
        private static readonly BaseFont BF_ITAL = BaseFont.CreateFont(BaseFont.HELVETICA_OBLIQUE, BaseFont.CP1252, false);

        // Convert "y from top" to iTextSharp bottom-origin y
        private static float Y(float yFromTop) => PH - yFromTop;

        // Measure text width
        private static float SW(string text, BaseFont bf, float size)
            => bf.GetWidthPoint(text, size);

        // ================================================================
        public async Task<byte[]> GeneratePrintStudentPdf(GetStudentByIdResponse model)
        {
            using var ms = new MemoryStream();

            var doc = new Document(new iTextSharp.text.Rectangle(PW, PH), 0, 0, 0, 0);
            var writer = PdfWriter.GetInstance(doc, ms);
            doc.Open();

            PdfContentByte cb = writer.DirectContent;

            float cy = MT; // cursor: y distance from top

            // ── HEADER ───────────────────────────────────────────────────
            cy = DrawCenteredText(cb, "WASHINGTONVILLE CENTRAL SCHOOL DISTRICT", cy, BF_BOLD, 11, CBlack, underline: true);
            cy += 30;
            cy = DrawCenteredText(cb, "APPLICATION FOR TRANSPORTATION", cy, BF_BOLD, 10, CBlack);
            cy += 13;
            cy = DrawCenteredText(cb, "Based Upon Section 3635 of the Education Law for Transportation, Requests for Transportation Must Be Made", cy, BF_REG, 7.5f, CBlack);
            cy += 12;
            cy = DrawCenteredText(cb, "by APRIL 1, 2026", cy, BF_BOLD, 9, CBlack, underline: true);
            cy += 25;
            cy = DrawCenteredText(cb, "I HEREBY REQUEST TRANSPORTATION FOR THE 2026/2027 SCHOOL YEAR FOR MY CHILD NAMED BELOW:", cy, BF_BOLD, 8.5f, CBlack);
            cy += 15;

            // ── BOX 1: STUDENT INFO ──────────────────────────────────────
            float b1Top = cy;
            cy += PAD + 20;

            // Name of Pupil / DOB / SEX
            //DrawText(cb, "Name of Pupil", LX, cy, BF_REG, 8.5f, CBlack);
            //float nameEnd = LX + SW("Name of Pupil", BF_REG, 8.5f) + 2;
            //DrawHLine(cb, nameEnd, cy + 1, LX + 215);

            DrawText(cb, "Name of Pupil", LX, cy, BF_REG, 8.5f, CBlack);
            float nameEnd = LX + SW("Name of Pupil", BF_REG, 8.5f) + 2;
            float nameLineEnd = LX + 215;
            DrawHLine(cb, nameEnd, cy + 1, nameLineEnd);
            // dynamic value
            string pupilName = $"{model?.FirstName} {model?.LastName}";
            DrawText(cb, pupilName, nameEnd + 3, cy - 2, BF_BOLD, 8.5f, CBlack);

            DrawText(cb, "DOB", LX + 223, cy, BF_REG, 8.5f, CBlack);

            float dobStart = LX + 223 + SW("DOB", BF_REG, 8.5f) + 2;
            float dobEnd = LX + 362;

            DrawHLine(cb, dobStart, cy + 1, dobEnd);

            // dynamic DOB
            string dob = model?.DOB?.ToString("MM/dd/yyyy");
            DrawText(cb, dob, dobStart + 3, cy - 2, BF_BOLD, 8.5f, CBlack);


            DrawText(cb, "SEX", LX + 370, cy, BF_REG, 8.5f, CBlack);

            float sexStart = LX + 370 + SW("SEX", BF_REG, 8.5f) + 2;

            DrawHLine(cb, sexStart, cy + 1, RX);

            // dynamic sex
            DrawText(cb, model?.Gender, sexStart + 3, cy - 2, BF_BOLD, 8.5f, CBlack);
            cy += 20;

            // Ethnicity
            // Ethnicity
            float ex = LX + SW("Ethnicity", BF_REG, 8.5f) + 4;
            DrawText(cb, "Ethnicity", LX, cy, BF_REG, 8.5f, CBlack);

            string[] ethLabels =
            {
                "White (non-Hispanic)",
                "Black (non-Hispanic)",
                "Hispanic",
                "Asian (Pacific Islander)",
                "American Indian (Alaskan Native)"
            };

            BaseFont tickWhiteFont = BaseFont.CreateFont(BaseFont.ZAPFDINGBATS, BaseFont.WINANSI, BaseFont.EMBEDDED);

            foreach (string lbl in ethLabels)
            {
                DrawHLine(cb, ex, cy + 1, ex + 18);

                // Tick for White
                if (lbl == "White (non-Hispanic)")
                {
                    cb.BeginText();
                    cb.SetFontAndSize(tickWhiteFont, 10);
                    cb.SetTextMatrix(ex + 4, Y(cy));
                    cb.ShowText("4"); // tick symbol
                    cb.EndText();
                }

                DrawText(cb, lbl, ex + 20, cy, BF_ITAL, 6.8f, CBlack);

                ex += 20 + SW(lbl, BF_ITAL, 6.8f) + 5;
            }

            cy += 20;

            // Street Address
            DrawText(cb, "Street Address", LX, cy, BF_REG, 8.5f, CBlack);

            float addrStart = LX + SW("Street Address", BF_REG, 8.5f) + 2;

            DrawHLine(cb, addrStart, cy + 1, RX);

            // dynamic address
            //DrawText(cb, model?.Address + ", " + model?.City + ", " + model?.State + ", " + model?.Zipcode + ", " + " Unit No: " + model?.Unit ?? "-", addrStart + 3, cy - 2, BF_BOLD, 8.5f, CBlack);
            string address =
            $"{model?.Address}, {model?.City}, {model?.State}, {model?.Zipcode}, Unit No: {model?.Unit}";

            DrawText(cb, address ?? "-", addrStart + 3, cy - 2, BF_BOLD, 8.5f, CBlack);
            cy += 10;
            DrawCenteredText(cb, "(Or actual location of residence/closest intersection)", cy, BF_ITAL, 7, CBlack);
            cy += 25;

            // Names of Parents / Mailing Address
            DrawText(cb, "Names of Parents/Guardians", LX, cy, BF_REG, 8.5f, CBlack);
            float parentStart = LX + SW("Names of Parents/Guardians", BF_REG, 8.5f) + 2;

            DrawHLine(cb, parentStart, cy + 1, RX);

            string parents = $"{model?.FatherFirstName} AND {model?.MotherFirstName}";
            DrawText(cb, parents, parentStart + 3, cy - 2, BF_BOLD, 8.5f, CBlack);
            cy += 20;
            //DrawText(cb, "Mailing Address", LX, cy, BF_REG, 8.5f, CBlack);
            //DrawHLine(cb, LX + SW("Mailing Address", BF_REG, 8.5f) + 2, cy + 1, RX);

            DrawText(cb, "Mailing Address", LX, cy, BF_REG, 8.5f, CBlack);

            float mailStart = LX + SW("Mailing Address", BF_REG, 8.5f) + 2;

            DrawHLine(cb, mailStart, cy + 1, RX);

            // Dynamic value
            string mailingAddress = model?.Address + ", " + model?.City + ", " + model?.State + ", " + model?.Zipcode + ", " + " Unit No: " + model?.Unit ?? "-";

            DrawText(cb, mailingAddress, mailStart + 3, cy - 2, BF_BOLD, 8.5f, CBlack);

            cy += 20;

            // Home / Work
            //DrawText(cb, "Home #", LX, cy, BF_REG, 8.5f, CBlack);
            //DrawHLine(cb, LX + SW("Home #", BF_REG, 8.5f) + 2, cy + 1, LX + 218);
            DrawText(cb, "Home #", LX, cy, BF_REG, 8.5f, CBlack);

            float homeStart = LX + SW("Home #", BF_REG, 8.5f) + 2;
            float homeEnd = LX + 218;

            DrawHLine(cb, homeStart, cy + 1, homeEnd);

            // Dynamic value
            string homePhone = model?.HomeNumber ?? model?.FatherCell ??  "";

            DrawText(cb, homePhone, homeStart + 3, cy - 2, BF_BOLD, 8.5f, CBlack);
            DrawText(cb, "Work #", LX + 226, cy, BF_REG, 8.5f, CBlack);
            DrawHLine(cb, LX + 226 + SW("Work #", BF_REG, 8.5f) + 2, cy + 1, RX);
            cy += 20;

            // Cell / E-Mail
            //DrawText(cb, "Cell #", LX, cy, BF_REG, 8.5f, CBlack);
            //DrawHLine(cb, LX + SW("Cell #", BF_REG, 8.5f) + 2, cy + 1, LX + 218);
            //DrawText(cb, "E-Mail", LX + 226, cy, BF_REG, 8.5f, CBlack);
            //DrawHLine(cb, LX + 226 + SW("E-Mail", BF_REG, 8.5f) + 2, cy + 1, RX);

            // Cell / E-Mail
            DrawText(cb, "Cell #", LX, cy, BF_REG, 8.5f, CBlack);

            float cellStart = LX + SW("Cell #", BF_REG, 8.5f) + 2;
            float cellEnd = LX + 218;

            DrawHLine(cb, cellStart, cy + 1, cellEnd);

            // Dynamic Cell number
            DrawText(cb, model?.MotherCell, cellStart + 3, cy - 2, BF_BOLD, 8.5f, CBlack);


            // EMAIL
            DrawText(cb, "E-Mail", LX + 226, cy, BF_REG, 8.5f, CBlack);

            float emailStart = LX + 226 + SW("E-Mail", BF_REG, 8.5f) + 2;

            DrawHLine(cb, emailStart, cy + 1, RX);

            // Dynamic Email
            DrawText(cb, model?.Email, emailStart + 3, cy - 2, BF_BOLD, 8.5f, CBlack);
            cy += 20;

            // Emergency Contact
            DrawText(cb, "Emergency Contact: Name", LX, cy, BF_REG, 8.5f, CBlack);
            DrawHLine(cb, LX + SW("Emergency Contact: Name", BF_REG, 8.5f) + 2, cy + 1, LX + 240);
            DrawText(cb, "Phone", LX + 248, cy, BF_REG, 8.5f, CBlack);
            DrawHLine(cb, LX + 248 + SW("Phone", BF_REG, 8.5f) + 2, cy + 1, RX);
            cy += 20;

            // School Attended
            //DrawText(cb, "School Attended 2025-2026", LX, cy, BF_REG, 8.5f, CBlack);
            //DrawHLine(cb, LX + SW("School Attended 2025-2026", BF_REG, 8.5f) + 2, cy + 1, RX);

            DrawText(cb, "School Attended 2025-2026", LX, cy, BF_REG, 8.5f, CBlack);

            float schoolStart = LX + SW("School Attended 2025-2026", BF_REG, 8.5f) + 2;

            DrawHLine(cb, schoolStart, cy + 1, RX);

            // Dynamic value
            string schoolAttended = model?.SchoolLegalName;

            DrawText(cb, schoolAttended, schoolStart + 3, cy - 2, BF_BOLD, 8.5f, CBlack);

            cy += 4;

            DrawBox(cb, ML, b1Top, CW, cy - b1Top + PAD);
            cy += 20;

            // ── BANNER ───────────────────────────────────────────────────
            float banH = 14f;
            DrawFilledRect(cb, ML, cy, CW, banH, CBlack);
            string banTxt = "PROOF OF RESIDENCY REQUIRED \u2013 TAX BILL/UTILITY BILL or LEASE AGREEMENT";
            float banTw = SW(banTxt, BF_BOLD, 8.5f);
            cb.SetColorFill(CWhite);
            cb.BeginText();
            cb.SetFontAndSize(BF_BOLD, 8.5f);
            cb.SetTextMatrix(PW / 2 - banTw / 2, Y(cy + banH / 2 + 3.5f));
            cb.ShowText(banTxt);
            cb.EndText();
            cy += banH + 10;

            // ── BOX 2: SCHOOL INFO (draw box FIRST, then content) ────────
            float b2Top = cy;
            // Pre-computed box height: pad + intro(11) + school_name(11) + school_addr(8) + street_label(13) + city_state_zip(17) + bottom_pad(6)
            float b2H = 100f;
            DrawBox(cb, ML, b2Top, CW, b2H);
            cy += 20f;

            // Intro mixed-font line
            float px2 = LX;
            (string text, BaseFont bf)[] introChunks =
            {
                ("NAME of SCHOOL ",              BF_BOLD),
                ("(PRIVATE/PAROCHIAL) to which ", BF_REG),
                ("TRANSPORTATION IS REQUESTED ", BF_BOLD),
                ("for ",                          BF_REG),
                ("2026-2027",                     BF_BOLD),
            };
            cb.SetColorFill(CBlack);
            cb.BeginText();
            foreach (var (chunk, bf) in introChunks)
            {
                cb.SetFontAndSize(bf, 8.5f);
                cb.SetTextMatrix(px2, Y(cy));
                cb.ShowText(chunk);
                px2 += SW(chunk, bf, 8.5f);
            }
            cb.EndText();
            cy += 20;

            // SCHOOL NAME / GRADE
            //DrawText(cb, "SCHOOL NAME", LX, cy, BF_REG, 8.5f, CBlack);
            //DrawHLine(cb, LX + SW("SCHOOL NAME", BF_REG, 8.5f) + 2, cy + 1, LX + 333);

            DrawText(cb, "SCHOOL NAME", LX, cy, BF_REG, 8.5f, CBlack);

            float schoolNameStart = LX + SW("SCHOOL NAME", BF_REG, 8.5f) + 2;
            float schoolNameEnd = LX + 333;

            DrawHLine(cb, schoolNameStart, cy + 1, schoolNameEnd);

            // Dynamic value
            string schoolName = model?.SchoolLegalName;

            DrawText(cb, schoolName, schoolNameStart + 3, cy - 2, BF_BOLD, 8.5f, CBlack);

            DrawText(cb, "GRADE", LX + 341, cy, BF_REG, 8.5f, CBlack);
            DrawHLine(cb, LX + 341 + SW("GRADE", BF_REG, 8.5f) + 2, cy + 1, RX);
            cy += 20;

            // SCHOOL ADDRESS
            DrawText(cb, "SCHOOL ADDRESS", LX, cy, BF_REG, 8.5f, CBlack);
            DrawHLine(cb, LX + SW("SCHOOL ADDRESS", BF_REG, 8.5f) + 2, cy + 1, RX);
            cy += 10;
            DrawCenteredText(cb, "STREET ADDRESS", cy, BF_ITAL, 7.5f, CBlack);
            cy += 15;

            //(float x, float w, string lbl)[] cityStateZip =
            //{
            //    (LX + 100,  152, "CITY"),
            //    (LX + 272,  112, "STATE"),
            //    (LX + 404,   91, "ZIP"),
            //};
            //foreach (var (x, w, lbl) in cityStateZip)
            //{
            //    DrawHLine(cb, x, cy, x + w);
            //    float tw = SW(lbl, BF_ITAL, 7.5f);
            //    DrawText(cb, lbl, x + w / 2 - tw / 2, cy + 9, BF_ITAL, 7.5f, CBlack);
            //}

            (string value, float x, float w, string lbl)[] cityStateZip =
            {
                ("Monroe",  LX + 100, 152, "CITY"),
                ("NY", LX + 272, 112, "STATE"),
                ("10950",   LX + 404,  91, "ZIP"),
            };

            foreach (var (value, x, w, lbl) in cityStateZip)
            {
                // draw line
                DrawHLine(cb, x, cy, x + w);

                // label under the line
                float tw = SW(lbl, BF_ITAL, 7.5f);
                DrawText(cb, lbl, x + w / 2 - tw / 2, cy + 9, BF_ITAL, 7.5f, CBlack);

                // dynamic value centered above line
                if (!string.IsNullOrWhiteSpace(value))
                {
                    float vw = SW(value, BF_BOLD, 8.5f);
                    DrawText(cb, value, x + w / 2 - vw / 2, cy - 2, BF_BOLD, 8.5f, CBlack);
                }
            }

            // Jump cy to bottom of box2
            cy = b2Top + b2H + 15;

            // ── KINDERGARTEN NOTE ─────────────────────────────────────────
            (string text, bool isRed, bool underline)[] kParts =
            {
                ("CHILDREN ENTERING ",  false, false),
                ("KINDERGARTEN",        true,  true),
                (", MUST BE ",          false, false),
                ("FIVE YEARS of AGE",   true,  false),
                (" by ",                false, false),
                ("DECEMBER 1st",        true,  false),
            };
            float totalKW = 0;
            foreach (var (t, _, _) in kParts) totalKW += SW(t, BF_BOLD, 8.5f);
            float kx = PW / 2 - totalKW / 2;
            cb.BeginText();
            foreach (var (t, isRed, _) in kParts)
            {
                cb.SetColorFill(isRed ? CRed : CBlack);
                cb.SetFontAndSize(BF_BOLD, 8.5f);
                cb.SetTextMatrix(kx, Y(cy));
                cb.ShowText(t);
                kx += SW(t, BF_BOLD, 8.5f);
            }
            cb.EndText();
            // underlines for KINDERGARTEN
            kx = PW / 2 - totalKW / 2;
            foreach (var (t, isRed, ul) in kParts)
            {
                if (ul)
                {
                    float tw = SW(t, BF_BOLD, 8.5f);
                    cb.SetColorStroke(isRed ? CRed : CBlack);
                    cb.SetLineWidth(0.4f);
                    cb.MoveTo(kx, Y(cy) - 1.5f);
                    cb.LineTo(kx + tw, Y(cy) - 1.5f);
                    cb.Stroke();
                }
                kx += SW(t, BF_BOLD, 8.5f);
            }
            cy += 13;

            // ── SIGNATURE BOX ─────────────────────────────────────────────
            float sigTop = cy;
            float sigH = 60f;
            DrawBox(cb, ML, sigTop, CW, sigH);
            //float sly = cy + 36;
            //DrawHLine(cb, LX, sly, LX + 148);
            //float dateTw = SW("DATE", BF_ITAL, 7.5f);
            //DrawText(cb, "DATE", LX + 74 - dateTw / 2, sly + 9, BF_ITAL, 7.5f, CBlack);

            float sly = cy + 36;

            DrawHLine(cb, LX, sly, LX + 148);

            float dateTw = SW("DATE", BF_ITAL, 7.5f);
            DrawText(cb, "DATE", LX + 74 - dateTw / 2, sly + 9, BF_ITAL, 7.5f, CBlack);

            // Today's date
            string todayDate = DateTime.Today.ToString("MM/dd/yyyy");

            // center the date on the line
            float dateWidth = SW(todayDate, BF_BOLD, 8.5f);

            DrawText(cb, todayDate, LX + 74 - dateWidth / 2, sly - 2, BF_BOLD, 8.5f, CBlack);

            float sigSX = LX + 183;
            float sigSW = CW - 183 - PAD * 2;
            DrawHLine(cb, sigSX, sly, sigSX + sigSW);
            string sigLbl = "SIGNATURE OF PARENT/GUARDIAN";
            float sigLblTw = SW(sigLbl, BF_ITAL, 7.5f);
            DrawText(cb, sigLbl, sigSX + sigSW / 2 - sigLblTw / 2, sly + 9, BF_ITAL, 7.5f, CBlack);
            cy = sigTop + sigH + 25;

            // ── DOCUMENTS NEEDED ──────────────────────────────────────────
            cy = DrawCenteredText(cb, "D O C U M E N T S   N E E D E D   b y   A P R I L   1 ,   2 0 2 6",
                cy, BF_BOLD, 9, CBlack, underline: true);
            cy += 20;
            float PAGE_WIDTH = writer.PageSize.Width;
            BaseFont tickFont = BaseFont.CreateFont(BaseFont.ZAPFDINGBATS, BaseFont.WINANSI, BaseFont.EMBEDDED);

            // ---------------- BULLET 1 ----------------

            (string t, BaseFont bf, bool underline)[] b1 =
            {
                ("BIRTH CERTIFICATE",  BF_BOLD, true),
                (" needed for ",       BF_REG, false),
                ("NEW STUDENTS",       BF_BOLD, false),
                (" and ",              BF_REG, false),
                ("KINDERGARTENERS",    BF_BOLD, false),
            };

            float totalWidth = 12;
            foreach (var (t, bf, _) in b1)
            {
                totalWidth += SW(t, bf, 8.5f);
            }

            float bx = (PAGE_WIDTH - totalWidth) / 2;

            // Draw tick
            cb.BeginText();
            cb.SetFontAndSize(tickFont, 10);
            cb.SetTextMatrix(bx, Y(cy));
            cb.ShowText("4");
            cb.EndText();

            float bpx = bx + 12;

            foreach (var (t, bf, underline) in b1)
            {
                cb.BeginText();
                cb.SetFontAndSize(bf, 8.5f);
                cb.SetTextMatrix(bpx, Y(cy));
                cb.ShowText(t);
                cb.EndText();

                float width = SW(t, bf, 8.5f);

                if (underline)
                {
                    cb.SetLineWidth(0.5f);
                    cb.MoveTo(bpx, Y(cy) - 2);
                    cb.LineTo(bpx + width, Y(cy) - 2);
                    cb.Stroke();
                }

                bpx += width;
            }

            cy += 15;


            // ---------------- BULLET 2 ----------------

            (string t, BaseFont bf, bool underline)[] b2 =
                        {
                ("PROOF of RESIDENCY", BF_BOLD, true),
                (" needed ",           BF_REG, false),
                ("(such as Lease, Utility Bill, Pay Stub, Photo ID)", BF_ITAL, false),
            };

            float totalWidth2 = 12;
            foreach (var (t, bf, _) in b2)
            {
                totalWidth2 += SW(t, bf, 8.5f);
            }

            bx = (PAGE_WIDTH - totalWidth2) / 2;

            // Tick
            cb.BeginText();
            cb.SetFontAndSize(tickFont, 10);
            cb.SetTextMatrix(bx, Y(cy));
            cb.ShowText("4");
            cb.EndText();

            bpx = bx + 12;

            foreach (var (t, bf, underline) in b2)
            {
                cb.BeginText();
                cb.SetFontAndSize(bf, 8.5f);
                cb.SetTextMatrix(bpx, Y(cy));
                cb.ShowText(t);
                cb.EndText();

                float width = SW(t, bf, 8.5f);

                if (underline)
                {
                    cb.SetLineWidth(0.5f);
                    cb.MoveTo(bpx, Y(cy) - 2);
                    cb.LineTo(bpx + width, Y(cy) - 2);
                    cb.Stroke();
                }

                bpx += width;
            }
            cy += 30;

            // ── MAIL TO ───────────────────────────────────────────────────
            (string t, float sz, BaseFont bf)[] mailParts =
            {
                ("Mail to: ",                      11, BF_REG),
                ("OFFICE of CENTRAL REGISTRATION", 16, BF_BOLD),
            };
            float totalMW = 0;
            foreach (var (t, sz, bf) in mailParts) totalMW += SW(t, bf, sz);
            float mx = PW / 2 - totalMW / 2;
            cb.BeginText();
            foreach (var (t, sz, bf) in mailParts)
            {
                cb.SetColorFill(CBlack); cb.SetFontAndSize(bf, sz);
                cb.SetTextMatrix(mx, Y(cy)); cb.ShowText(t);
                mx += SW(t, bf, sz);
            }
            cb.EndText();
            cy += 20;

            DrawCenteredText(cb, "52 West Main Street", cy, BF_BOLD, 12, CBlack); cy += 14;
            DrawCenteredText(cb, "Washingtonville, NY  10992", cy, BF_BOLD, 12, CBlack); cy += 25;
            DrawCenteredText(cb, "USE A SEPARATE FORM FOR EACH CHILD", cy, BF_BOLD, 11, CRed); cy += 13;
            DrawCenteredText(cb, "Forms will not be accepted without proof of residency", cy, BF_ITAL, 8.5f, CBlack);

            doc.Close();
            return ms.ToArray();
        }

        // ================================================================
        // DRAWING PRIMITIVES
        // ================================================================

        /// <summary>Draw left-aligned text. Returns x-advance (unused here but handy).</summary>
        //private static float DrawText(PdfContentByte cb, string text, float x, float yFromTop,
        //    BaseFont bf, float size, BaseColor color)
        //{
        //    cb.SetColorFill(color);
        //    cb.BeginText();
        //    cb.SetFontAndSize(bf, size);
        //    cb.SetTextMatrix(x, Y(yFromTop));
        //    cb.ShowText(text);
        //    cb.EndText();
        //    return SW(text, bf, size);
        // }
        private static float DrawText(PdfContentByte cb, string text, float x, float yFromTop,
     BaseFont bf, float size, BaseColor color)
        {
            text ??= "";   // prevents null exception

            cb.SetColorFill(color);
            cb.BeginText();
            cb.SetFontAndSize(bf, size);
            cb.SetTextMatrix(x, Y(yFromTop));
            cb.ShowText(text);
            cb.EndText();

            return SW(text, bf, size);
        }

        /// <summary>Draw centered text, optionally underlined. Returns same y (for chaining).</summary>
        private static float DrawCenteredText(PdfContentByte cb, string text, float yFromTop,
            BaseFont bf, float size, BaseColor color, bool underline = false)
        {
            float tw = SW(text, bf, size);
            float x = PW / 2 - tw / 2;
            cb.SetColorFill(color);
            cb.BeginText();
            cb.SetFontAndSize(bf, size);
            cb.SetTextMatrix(x, Y(yFromTop));
            cb.ShowText(text);
            cb.EndText();
            if (underline)
            {
                cb.SetColorStroke(color);
                cb.SetLineWidth(0.5f);
                cb.MoveTo(x, Y(yFromTop) - 1.5f);
                cb.LineTo(x + tw, Y(yFromTop) - 1.5f);
                cb.Stroke();
            }
            return yFromTop;
        }

        /// <summary>Horizontal rule.</summary>
        private static void DrawHLine(PdfContentByte cb, float x1, float yFromTop, float x2, float lw = 0.5f)
        {
            cb.SetColorStroke(new BaseColor(0, 0, 0));
            cb.SetLineWidth(lw);
            cb.MoveTo(x1, Y(yFromTop));
            cb.LineTo(x2, Y(yFromTop));
            cb.Stroke();
        }

        /// <summary>Unfilled rectangle border (box).</summary>
        private static void DrawBox(PdfContentByte cb, float x, float yFromTop, float w, float h, float lw = 1.2f)
        {
            cb.SetColorStroke(new BaseColor(0, 0, 0));
            cb.SetLineWidth(lw);
            cb.Rectangle(x, Y(yFromTop + h), w, h);
            cb.Stroke();
        }

        /// <summary>Filled rectangle (used for banner).</summary>
        private static void DrawFilledRect(PdfContentByte cb, float x, float yFromTop, float w, float h, BaseColor color)
        {
            cb.SetColorFill(color);
            cb.Rectangle(x, Y(yFromTop + h), w, h);
            cb.Fill();
        }
    }
}