# Municipal Services Application

A Windows Forms application that allows South African residents to report municipal
service issues, such as water leaks, potholes and uncollected refuse, to their local
municipality.

This is Part 1 of the Portfolio of Evidence. Only the **Report Issues** feature is
implemented at this stage. **Local Events and Announcements** and **Service Request
Status** appear on the main menu but are disabled, and will be implemented in later
parts.

---

## 1. Requirements

| Requirement | Version |
|---|---|
| Operating system | Windows 10 or Windows 11 |
| IDE | Visual Studio 2022 (17.8 or later) |
| Framework | .NET 8.0 |
| Workload | .NET desktop development |
| Language | C# |

---

## 2. How to compile

### Using Visual Studio

1. Open `MunicipalServicesApp.sln` in Visual Studio 2022.
2. Wait for the solution to finish loading in Solution Explorer.
3. Select **Build > Build Solution**, or press **F6**.
4. Confirm that the Output window reports `Build succeeded` with 0 errors.

### Using the command line

From the folder containing the solution file:

```
dotnet build
```

---

## 3. How to run

### Using Visual Studio

Press **F5** to run with debugging, or **Ctrl + F5** to run without debugging.

### Using the command line

```
dotnet run --project MunicipalServicesApp
```

### Running the compiled executable

After a successful build, the executable is located at:

```
MunicipalServicesApp\bin\Debug\net8.0-windows\MunicipalServicesApp.exe
```

---

## 4. How to use the software

### 4.1 Main menu

The main menu appears on startup and presents three municipal services:

- **Report Issues** — available.
- **Local Events and Announcements** — greyed out, to be implemented later.
- **Service Request Status** — greyed out, to be implemented later.

A summary at the bottom of the window shows how many issues have been reported during
the current session. Click **Exit** to close the application; a confirmation prompt is
shown before it closes.

### 4.2 Reporting an issue

Click **Report Issues** to open the reporting form, then complete the following:

1. **Location** — type where the issue is, for example `Corner of Main Road and 5th Avenue, Kempton Park`. Required, minimum 3 characters.
2. **Category** — choose from the dropdown list: Sanitation, Roads and Potholes, Water and Sewerage, Electricity, Refuse Removal, Street Lighting, Parks and Public Spaces, or Other. Required.
3. **Description** — describe the issue in the rich text box. Required, minimum 10 characters.
4. **Attachment** — click **Attach Photo or Document** to open a file dialog and select a supporting image or document. Optional, maximum file size 10 MB. Accepted formats are PNG, JPG, JPEG, BMP, GIF, PDF, DOCX, DOC and TXT.

Then click **Submit Report**.

Other controls on this form:

- **Clear** — empties all fields after a confirmation prompt.
- **Back to Main Menu** — closes the form and returns to the main menu.

### 4.3 User engagement feature

The application implements **gamification through progressive feedback** as its user
engagement strategy.

A progress bar at the top of the reporting form fills as each of the four stages is
completed, in 25% increments:

| Stage completed | Progress |
|---|---|
| Location entered | 25% |
| Category selected | 50% |
| Description entered | 75% |
| Attachment added | 100% |

Above the bar, a message updates in real time to acknowledge what has been completed and
name the next useful step, for example *"Almost there. Describe the issue so the team
knows what to expect."*

On successful submission, a confirmation message displays a unique reference number in
the format `MSA-YYYY-NNNN` (for example `MSA-2026-0001`), together with an
acknowledgement of how many reports the resident has submitted during the session.

### 4.4 Validation and feedback

The application will not accept an incomplete report. If a required field is missing or
too short, a message explains what is needed and the cursor is placed in the field that
requires attention. Successful actions, warnings and errors are all communicated through
message boxes.

---

## 5. Data storage

Reported issues are stored in memory in a `List<Issue>` managed by the static
`IssueManager` class. A `List` was chosen because reports are added sequentially, the
order of submission is meaningful, the number of reports is not known in advance, and the
full collection is read back as a whole when displaying reports.

**Reports are not written to disk.** All data is held for the duration of the session and
is cleared when the application closes. Persistent storage is outside the scope of
Part 1.

---

## 6. Project structure

```
MunicipalServicesApp/
├── Models/
│   └── Issue.cs                  Represents a single reported issue
├── Services/
│   ├── IssueManager.cs           Stores all issues in a List and issues reference numbers
│   └── EngagementTracker.cs      Progress calculation and feedback messages
├── UI/
│   └── Theme.cs                  Shared colour scheme, fonts and button styles
├── MainMenuForm.cs               Main menu presented on startup
├── ReportIssuesForm.cs           Issue reporting form
└── Program.cs                    Application entry point
```

---

## 7. Known limitations

- Reported issues are not saved between sessions.
- Attachments are recorded by file path only; the file itself is not copied or embedded.
- Reference numbers restart from `0001` each time the application is launched.
- Local Events and Announcements and Service Request Status are intentionally disabled.

---

## 8. Troubleshooting

**The solution will not build, and the target framework is reported as missing.**
Install the .NET 8.0 SDK, then reopen the solution.

**The designer will not open for a form.**
Build the solution first (F6). The forms style themselves from `Theme.cs` at runtime, so
the project must compile before the designer can load them.

**An attached file cannot be read.**
Confirm the file still exists at its original location and is not open in another
application.

---

## 9. Author

| Lesego Letsapa |
| ST10351970 |
| Module | Advanced Application Development XADAD7112w |
| Assessment | Part 1, Portfolio of Evidence |
