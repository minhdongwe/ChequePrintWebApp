# 🧾 ChequePrintWebApp

A full-featured cheque printing and Provident Fund (PF) management system built in **ASP.NET Core MVC**.  
This system is designed for Finance departments to generate printable cheques, calculate PF deductions, and maintain cheque history with export capabilities.

---

## ✨ Key Features

- 🖨️ **Cheque Generation** — Automatically fetch employee data from Excel and generate formatted cheques
- 📋 **PF Calculator** — Real-time deduction calculation with Grand Total update
- 🔍 **Search & Filter** — Filter cheque history by employee name, personal number, and date range
- 📥 **Excel Export** — Download full or filtered cheque history in Excel format using EPPlus
- 🧾 **Print-Ready Layout** — Designed for printing on real cheque stationery (precise layout using `cm`)
- 💾 **Local JSON Storage** — All data saved locally in `App_Data/history.json` for persistence
- 🛠️ **Ready for SQLite** — Structure designed to easily switch to Entity Framework + SQLite

---

## 🛠️ Technologies Used

- ASP.NET Core 8.0 (MVC)
- C# and Razor Views
- EPPlus for Excel Integration
- JavaScript (Vanilla)
- JSON Serialization for local data storage
- Bootstrap 5 for styling
- Git & GitHub for version control

---

## 📁 Sample Usage

- Sample employees included in `wwwroot/files/Employees.xlsx`
- Use any 6-digit **Personal Number** (e.g. `123456`, `456789`) from the sheet to:
  - Generate a cheque
  - Simulate PF calculation
- All generated cheques are saved and shown in the **History** tab

---

## 🧠 Folder Structure

Controllers/
Views/
wwwroot/
App_Data/ # JSON storage for cheque history
files/ # Excel data file (Employees.xlsx & PFFile1.xlsx)
Models/ # ViewModels and Data Models


---

## ⚠️ Disclaimer

- This is a **sample version** for demo/portfolio use  
- Original employee data has been removed for privacy  
- You can modify the Excel sheet and re-upload for your organization

---

## 📌 Author

**Ali Mohtisham Shoukat**
amsk91@hotmail.com
923454640268
Built for learning, finance automation, and real-world application  
Feel free to connect or fork for your own needs!

---

