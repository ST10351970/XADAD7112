# Municipal Services Application

A C# Windows Forms application for South African residents to report municipal issues,
find local events and service notices, and track the progress of their service requests.

---

## 1. Requirements

IDE: Visual Studio 2022
Framework: .NET 8.0

---

## 2. How to compile

**Visual Studio**

1. Open `MunicipalServicesApp.sln`.
2. Select **Build > Rebuild Solution**.
3. Check that the Output window shows `Build succeeded` with 0 errors.

**Command line** (in the project folder):

```
dotnet build MunicipalServicesApp.sln
```

---

## 3. How to run

- **Visual Studio:** press **F5** 
- **Command line:** `dotnet run --project MunicipalServicesApp.csproj`
- **Executable:** after building, run `bin\Debug\net8.0-windows\MunicipalServicesApp.exe`.

All data is kept in memory and is cleared when the application closes. Sample events,
notices and service requests are loaded when it starts, with dates relative to today.

---

## 4. How to use the software

### 4.1 Main menu

Choose one of three services: **Report Issues**, **Local Events and Announcements** or
**Service Request Status**. The text beside each button shows live information, such as
the most urgent notice and the most urgent open request. **Exit** closes the application
after asking you to confirm.

### 4.2 Report Issues

1. **Location:** where the issue is, e.g. `14 Burnett Street, Hatfield` (required).
2. **Category:** choose from the list (required).
3. **Description:** what is wrong, at least 10 characters (required).
4. **Attach Photo or Document:** optional; images, PDF, Word or text files up to 10 MB.
5. Click **Submit Report**.

The progress bar and message at the top fill up as you complete each step. After you
submit, a confirmation shows your **reference number** (e.g. `MSA-2026-0027`). Keep it:
you can use it to track the report on the Service Request Status page. **Clear** empties
the form and **Back to Main Menu** returns to the menu.

### 4.3 Local Events and Announcements

- **Service notices** (left) are listed most urgent first. **Events** (centre) are grouped
  by day.
- **Search:** enter a keyword, choose a category, and/or tick **From** and **To** to set
  dates, then click **Search** or press **Enter**.
- **Previous search** goes back one search at a time. **Clear** shows everything again.
- Click a day on the **calendar** (days with events are bold) to see that day's events.
- Click an event card to see its full details.
- **Recommended for you** (right) suggests events based on what you search for and open.
  Before you search, it shows events coming up soon.

### 4.4 Service Request Status

**Track a request:** type a reference number (e.g. `MSA-2026-0006`, or just `6` for this
year) and click **Track** or press **Enter**. The request is selected in the list and
opened on the **Details** tab.

**Browse and filter the list:**

| Control | What it does |
|---|---|
| Department tree (left) | Click a department or category to list only its requests; click a request to open it. |
| Sort | Newest first, oldest first, or by reference number. |
| Status | All, open, waiting for a crew, blocked, or resolved/closed. |
| Suburb starts with | e.g. `S` shows Silverton and Sunnyside. |
| Submitted from / to | Tick a box to use that date. |
| Clear filters | Removes all filters. |

Requests you reported in this session are marked `*`. Status colours: grey = Submitted,
blue = Acknowledged, purple = Assigned, orange = In progress, green = Resolved,
slate = Closed. A request shown as **(blocked)** in red is waiting for another request.

**Tabs on the right:**

| Tab | What you can do |
|---|---|
| **Details** | See the request's information, a timeline of its stages and its history. **Advance to next stage** moves it on (a staff demonstration; a blocked request cannot start work). |
| **Urgent queue** | See the 10 most urgent requests waiting for a crew. **Dispatch next job** assigns the most urgent one that can start. Double-click a row to open it. |
| **Related** | See which requests the selected one is waiting on, which are waiting on it, and the order to resolve them. Double-click a row to open it. |
| **Crew route** | Choose a department to see a route linking the depot to every suburb with open requests. Stops are numbered in visiting order; hover over a suburb for details. **Open larger map** shows it full size (Escape closes it). |
| **Under the hood** | See the data structures behind the page and the results of automatic self-checks. Click **Run checks again** after making changes. |

**Quick tour:**

1. Track `MSA-2026-0008`. It is blocked, and the **Related** tab shows it waiting on
   `MSA-2026-0007` and `MSA-2026-0006`.
2. Track `MSA-2026-0006` and click **Advance to next stage**. It becomes Resolved, and
   `MSA-2026-0007` is no longer blocked.
3. On the **Urgent queue** tab, click **Dispatch next job**.
4. On the **Crew route** tab, choose *Roads and Transport*.
5. Report a new issue on the Report Issues page, then track its reference number here.

**Back to Main Menu** (or **Escape**) returns to the menu.

---

## 5. Report

A **binary tree** is the base class for all 3 search trees in the app. It holds the height calculation and the traversals (in-order, reverse in-order, pre-order, post-order and level-order). An in-order traversal lists requests in sorted order without a separate sort, and a reverse in-order traversal produces the “Newest first” view”.

A **binary search tree (BST)** indexes requests by suburb name. The “Suburb starts with” filter is a range query on this tree, so typing “S” only visits the branch that can match (Silverton, Sunnyside). An unbalanced BST is also kept by reference number, purely for comparison with AVL tree.

An **AVL tree** is the main index by reference number, so tracking a request take O(log n) comparisons. It is needed because reference numbers are issued in sequence, which turns a plain BST into a chain. Tracking the newest request takes 5 comparisons in the AVL tree and 31 in the unbalanced BST. The tree also issues new reference numbers by finding the highest number already used this year.

A **red-block tree** indexes requests by submission date and time. It produces the “Newest first” and “Oldest first” lists, and the “Submitted from/to” filter is a range query that only visits requests inside the chosen dates. It’s re-colouring and rotations keep it balanced as requests arrive.

A **general (n-ary) tree** models the municipality as departments, then categories, then individual requests and drives the department browser. A pre-order traversal collects every request under a selected department. A post-order traversal adds up the open and total counts on each branch.

A **min-heap** holds the requests waiting for a crew, with the most urgent at the top: Critical before Low, then oldest first. It drives the Urgent queue tab, the “Dispatch next job” button and the “Most urgent” line on the main menu. Dijkstra’s and Prim’s algorithms also use a heap to pick the next closet suburb or cheapest road.

The use of **graphs** covers 2 models. A directed graph records which requests must wait for others, for example a road repair waiting on the burst pipe beneath it. This produces the “blocked” status and stops blocked work from being dispatched. A weighted, undirected graph holds 14 Tshwane suburbs joined by 24 roads, with distances in km and is used for crew routes.

The use of **graph traversal** is split between 2 algorithms. Breath-first search fills the Related tab, listing linked requests level by level. Depth-first search does 3 jobs:
1.	Working out the order in which linked requests must be resolved.
2.	Refusing any new dependency that would create a cycle.
3.	Turning the route tree into a visiting order for the crew.

A **minimum spanning tree (MST)** plans the crew route. Dijkstra’s algorithm finds the shortest road distances between the depot and each suburb with open requests. Prim’s algorithm then links those suburbs with the shortest total network which is drawn in green on the map. Kruskal’s algorithm, with a disjoint set, runs as a self-check and must find the same 70.7km tree for the full road network.

---

## 6. Author

Name: Lesego Letsapa
Student number: ST10351970
Module: Advanced Application Development (AAPD7112)
Assessment: Portfolio of Evidence
