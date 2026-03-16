<!DOCTYPE html>
<html lang="en">
<head>
<meta charset="UTF-8">
<meta name="viewport" content="width=device-width, initial-scale=1.0">
<title>Gym Management System</title>
<link href="https://fonts.googleapis.com/css2?family=DM+Mono:wght@400;500&family=Fraunces:opsz,wght@9..144,300;9..144,400;9..144,600&family=DM+Sans:wght@300;400;500&display=swap" rel="stylesheet">
<style>
```css
*{box-sizing:border-box;margin:0;padding:0}
:root{
  --ink:#18191c;
  --ink2:#3c404a;
  --ink3:#606470;
  --surface:#e4e4e4;
  --surface2:#d4d4d4;
  --surface3:#c4c4c4;
  --card:#eeeeee;
  --card-hover:#e0e0e0;
  --accent:#1a3a6b;
  --accent2:#2f5fa8;
  --accent-light:#ccdaf0;
  --grey:#4a5060;
  --grey2:#686c78;
  --grey-light:#d8d8d8;
  --rule:rgba(0,0,0,0.1);
  --mono:'DM Mono', monospace;
  --serif:'Fraunces', serif;
  --sans:'DM Sans', sans-serif;
}
body{
  font-family:var(--sans);
  background:var(--surface);
  color:var(--ink);
  line-height:1.6;
  padding:0 0 3rem;
  max-width:900px;
  margin:0 auto;
}

/* Hero */
.hero{
  border-bottom:1px solid var(--rule);
  padding:3rem 2.5rem 2.5rem;
  position:relative;
  overflow:hidden;
  background:#0f1929;
}
.hero::before{
  content:'';
  position:absolute;
  top:-80px;right:-80px;
  width:300px;height:300px;
  border-radius:50%;
  background:rgba(47,95,168,0.25);
}
.hero::after{
  content:'';
  position:absolute;
  bottom:-60px;left:30%;
  width:200px;height:200px;
  border-radius:50%;
  background:rgba(47,95,168,0.12);
}
.eyebrow{
  font-family:var(--mono);
  font-size:11px;
  letter-spacing:0.14em;
  color:#7aaae8;
  text-transform:uppercase;
  margin-bottom:1rem;
}
.hero h1{
  font-family:var(--serif);
  font-size:2.8rem;
  font-weight:300;
  line-height:1.15;
  color:#e8f0fb;
  margin-bottom:1rem;
  max-width:500px;
  position:relative;z-index:1;
}
.hero h1 em{
  font-style:italic;
  color:#7aaae8;
}
.hero-desc{
  font-size:15px;
  color:#8fa8c8;
  max-width:440px;
  margin-bottom:2rem;
  position:relative;z-index:1;
}
.badge-row{
  display:flex;
  flex-wrap:wrap;
  gap:8px;
  position:relative;z-index:1;
}
.badge{
  display:inline-flex;
  align-items:center;
  gap:5px;
  font-family:var(--mono);
  font-size:11px;
  color:#7aaae8;
  background:rgba(122,170,232,0.12);
  border:0.5px solid rgba(122,170,232,0.35);
  padding:4px 10px;
  border-radius:4px;
}
.demo-link{
  display:inline-flex;
  align-items:center;
  gap:6px;
  font-size:13px;
  font-family:var(--mono);
  color:#7aaae8;
  text-decoration:none;
  border-bottom:1px solid rgba(122,170,232,0.4);
  padding-bottom:2px;
  margin-bottom:2rem;
  position:relative;z-index:1;
}

/* Sections */
section{
  padding:2.5rem;
  border-bottom:1px solid var(--rule);
  background:var(--card);
}
section:nth-child(even){background:var(--surface)}
.section-label{
  font-family:var(--mono);
  font-size:10px;
  letter-spacing:0.14em;
  color:var(--ink3);
  text-transform:uppercase;
  margin-bottom:1.5rem;
}
.section-title{
  font-family:var(--serif);
  font-size:1.4rem;
  font-weight:300;
  color:var(--ink);
  margin-bottom:1rem;
}

/* Feature grid */
.feature-grid{
  display:grid;
  grid-template-columns:repeat(auto-fill,minmax(200px,1fr));
  gap:1px;
  background:var(--rule);
  border:1px solid var(--rule);
  border-radius:8px;
  overflow:hidden;
}
.feature-cell{
  background:var(--card);
  padding:1.25rem 1.5rem;
  transition:background 0.15s;
}
.feature-cell:hover{background:var(--card-hover)}
.feature-title{
  font-size:13px;
  font-weight:500;
  color:var(--ink);
  margin-bottom:4px;
}
.feature-desc{
  font-size:12px;
  color:var(--ink3);
  line-height:1.5;
}
.feature-dot{
  width:6px;height:6px;
  border-radius:50%;
  background:var(--grey2);
  margin-bottom:10px;
}

/* Architecture */
.arch-diagram{
  display:grid;
  grid-template-columns:1fr;
  gap:1px;
  background:var(--rule);
  border:1px solid var(--rule);
  border-radius:8px;
  overflow:hidden;
  margin-top:1.5rem;
}
.arch-layer{
  display:flex;
  align-items:stretch;
  background:var(--card);
  transition:background 0.15s;
}
.arch-layer:hover{background:var(--card-hover)}
.arch-tag{
  font-family:var(--mono);
  font-size:10px;
  letter-spacing:0.1em;
  text-transform:uppercase;
  writing-mode:vertical-rl;
  text-orientation:mixed;
  transform:rotate(180deg);
  padding:1.25rem 0.75rem;
  background:var(--surface2);
  color:var(--grey);
  border-right:1px solid var(--rule);
  white-space:nowrap;
  min-width:36px;
  display:flex;
  align-items:center;
  justify-content:center;
  font-weight:500;
}
.arch-content{
  padding:1.25rem 1.5rem;
  flex:1;
}
.arch-name{
  font-family:var(--mono);
  font-size:12px;
  color:var(--grey);
  margin-bottom:3px;
}
.arch-folder{
  font-size:13px;
  font-weight:500;
  color:var(--ink);
  margin-bottom:3px;
}
.arch-desc{
  font-size:12px;
  color:var(--ink3);
}

/* Steps */
.steps{
  display:flex;
  flex-direction:column;
  border:1px solid var(--rule);
  border-radius:8px;
  overflow:hidden;
  margin-top:1.5rem;
}
.step{
  display:flex;
  align-items:flex-start;
  gap:1.25rem;
  padding:1.25rem 1.5rem;
  background:var(--card);
  border-bottom:1px solid var(--rule);
  transition:background 0.15s;
}
.step:hover{background:var(--card-hover)}
.step:last-child{border-bottom:none}
.step-num{
  font-family:var(--mono);
  font-size:11px;
  color:var(--grey);
  background:var(--grey-light);
  border:0.5px solid rgba(90,96,112,0.25);
  border-radius:4px;
  width:24px;height:24px;
  display:flex;align-items:center;justify-content:center;
  flex-shrink:0;
  margin-top:1px;
}
.step-body{flex:1}
.step-title{
  font-size:13px;font-weight:500;
  color:var(--ink);margin-bottom:4px;
}
.step-desc{font-size:12px;color:var(--ink3);line-height:1.5}
.code-inline{
  font-family:var(--mono);
  font-size:11px;
  background:var(--surface2);
  color:var(--grey);
  padding:2px 6px;
  border-radius:3px;
  border:0.5px solid var(--rule);
}

/* Tech table */
.tech-table{width:100%;border-collapse:collapse;margin-top:1.5rem;font-size:13px;}
.tech-table th{
  font-family:var(--mono);font-size:10px;letter-spacing:0.12em;
  text-transform:uppercase;color:var(--ink3);
  text-align:left;padding:0.5rem 1rem;
  border-bottom:1px solid var(--rule);font-weight:400;
}
.tech-table td{
  padding:0.75rem 1rem;
  border-bottom:1px solid var(--rule);
  color:var(--ink2);
  vertical-align:top;
}
.tech-table tr:last-child td{border-bottom:none}
.tech-table td:first-child{font-family:var(--mono);font-size:12px;color:var(--grey);font-weight:500}
.tech-table tr:hover td{background:var(--card-hover)}

/* Credentials card */
.cred-card{
  background:var(--surface2);
  border:0.5px solid var(--rule);
  border-radius:8px;
  padding:1.25rem 1.5rem;
  margin-top:1.5rem;
  display:flex;
  align-items:flex-start;
  gap:1rem;
}
.cred-icon{
  width:32px;height:32px;
  background:var(--grey-light);
  border:0.5px solid rgba(90,96,112,0.25);
  border-radius:6px;
  display:flex;align-items:center;justify-content:center;
  color:var(--grey);font-size:14px;flex-shrink:0;
  font-weight:500;font-family:var(--mono);
}
.cred-label{font-size:11px;font-family:var(--mono);color:var(--ink3);text-transform:uppercase;letter-spacing:0.1em;margin-bottom:8px}
.cred-row{display:flex;align-items:center;gap:8px;margin-bottom:4px}
.cred-key{font-size:12px;color:var(--ink3);min-width:60px;font-family:var(--mono)}
.cred-val{font-family:var(--mono);font-size:12px;color:var(--ink);background:var(--card);padding:2px 8px;border-radius:3px;border:0.5px solid var(--rule)}

/* Author */
.author-strip{
  padding:2rem 2.5rem;
  display:flex;
  align-items:center;
  justify-content:space-between;
  background:var(--card);
}
.author-name{
  font-family:var(--serif);
  font-size:1.1rem;
  font-weight:300;
  color:var(--ink);
}
.author-sub{font-size:12px;color:var(--ink3);margin-top:2px;font-family:var(--mono)}
.gh-link{
  display:inline-flex;align-items:center;gap:6px;
  font-family:var(--mono);font-size:11px;
  color:var(--grey);text-decoration:none;
  border:0.5px solid rgba(90,96,112,0.35);
  padding:6px 12px;border-radius:4px;
  transition:background 0.15s;
}
.gh-link:hover{background:var(--card-hover)}
</style>
</head>
<body>
```

<!-- Hero -->
<div class="hero">
  <p class="eyebrow">Open Source Project</p>
  <h1>Gym Management <em>System</em></h1>
  <p class="hero-desc">A full-featured web application for managing gym operations — from member registration to trainer scheduling and analytics.</p>
  <a class="demo-link" href="http://powerfit.runasp.net" target="_blank">powerfit.runasp.net &rarr;</a>
  <div class="badge-row">
    <span class="badge">C#</span>
    <span class="badge">ASP.NET</span>
    <span class="badge">HTML / CSS</span>
    <span class="badge">JavaScript</span>
    <span class="badge">SQL Server</span>
    <span class="badge">SignalR</span>
  </div>
</div>

<!-- Features -->
<section>
  <p class="section-label">Capabilities</p>
  <div class="feature-grid">
    <div class="feature-cell">
      <div class="feature-dot"></div>
      <p class="feature-title">Member management</p>
      <p class="feature-desc">Add, edit, and track gym members across the full lifecycle</p>
    </div>
    <div class="feature-cell">
      <div class="feature-dot"></div>
      <p class="feature-title">Subscription plans</p>
      <p class="feature-desc">Define membership types, durations, and pricing tiers</p>
    </div>
    <div class="feature-cell">
      <div class="feature-dot"></div>
      <p class="feature-title">Trainer management</p>
      <p class="feature-desc">Assign trainers, manage sessions, and track schedules</p>
    </div>
    <div class="feature-cell">
      <div class="feature-dot"></div>
      <p class="feature-title">Payment tracking</p>
      <p class="feature-desc">Monitor invoices, payments, and membership renewals</p>
    </div>
    <div class="feature-cell">
      <div class="feature-dot"></div>
      <p class="feature-title">Reports and analytics</p>
      <p class="feature-desc">Generate insights on gym activity and revenue trends</p>
    </div>
    <div class="feature-cell">
      <div class="feature-dot"></div>
      <p class="feature-title">Authentication</p>
      <p class="feature-desc">Secure login flows for admins, staff, and members</p>
    </div>
    <div class="feature-cell">
      <div class="feature-dot"></div>
      <p class="feature-title">Support chat</p>
      <p class="feature-desc">Real-time messaging between members and staff via SignalR</p>
    </div>
    <div class="feature-cell">
      <div class="feature-dot"></div>
      <p class="feature-title">Session enrollment</p>
      <p class="feature-desc">Members can browse and enroll in scheduled group sessions</p>
    </div>
  </div>
</section>

<!-- Architecture -->
<section>
  <p class="section-label">Architecture</p>
  <p class="section-title">Three-layer separation of concerns</p>
  <div class="arch-diagram">
    <div class="arch-layer">
      <div class="arch-tag">Presentation</div>
      <div class="arch-content">
        <p class="arch-name">GymSystem/</p>
        <p class="arch-folder">UI, forms, and user interaction</p>
        <p class="arch-desc">All views, layouts, and client-facing interface components</p>
      </div>
    </div>
    <div class="arch-layer">
      <div class="arch-tag">Business Logic</div>
      <div class="arch-content">
        <p class="arch-name">GymSystemBLL/</p>
        <p class="arch-folder">Rules, validation, and workflows</p>
        <p class="arch-desc">Core application logic, business rules, and data orchestration</p>
      </div>
    </div>
    <div class="arch-layer">
      <div class="arch-tag">Data Access</div>
      <div class="arch-content">
        <p class="arch-name">GymSystemDAL/</p>
        <p class="arch-folder">Database queries and operations</p>
        <p class="arch-desc">All SQL Server interactions, stored procedures, and data mapping</p>
      </div>
    </div>
  </div>
</section>

<!-- Getting started -->
<section>
  <p class="section-label">Getting started</p>
  <p class="section-title">Installation</p>
  <div class="steps">
    <div class="step">
      <span class="step-num">1</span>
      <div class="step-body">
        <p class="step-title">Clone the repository</p>
        <p class="step-desc"><span class="code-inline">git clone https://github.com/EhabOkashahh/Gym-Management-System.git</span></p>
      </div>
    </div>
    <div class="step">
      <span class="step-num">2</span>
      <div class="step-body">
        <p class="step-title">Open the solution</p>
        <p class="step-desc">Open <span class="code-inline">GymSystem.sln</span> in Visual Studio 2019 or later</p>
      </div>
    </div>
    <div class="step">
      <span class="step-num">3</span>
      <div class="step-body">
        <p class="step-title">Configure the database</p>
        <p class="step-desc">Set up a SQL Server instance, then update the connection string in the DAL project</p>
      </div>
    </div>
    <div class="step">
      <span class="step-num">4</span>
      <div class="step-body">
        <p class="step-title">Build and run</p>
        <p class="step-desc">Press <span class="code-inline">F5</span> or click Run in Visual Studio</p>
      </div>
    </div>
  </div>

  <div class="cred-card">
    <div class="cred-icon">M</div>
    <div>
      <p class="cred-label">Demo account (member)</p>
      <div class="cred-row">
        <span class="cred-key">Email</span>
        <span class="cred-val"><a href="/cdn-cgi/l/email-protection" class="__cf_email__" data-cfemail="591d3c3436193e34383035773a3634">[email&#160;protected]</a></span>
      </div>
      <div class="cred-row">
        <span class="cred-key">Password</span>
        <span class="cred-val">Tcecaf8a8!</span>
      </div>
    </div>
  </div>
</section>

<!-- Tech stack -->
<section>
  <p class="section-label">Tech stack</p>
  <table class="tech-table">
    <thead>
      <tr>
        <th>Technology</th>
        <th>Purpose</th>
      </tr>
    </thead>
    <tbody>
      <tr><td>C#</td><td>Core application logic and server-side processing</td></tr>
      <tr><td>ASP.NET</td><td>Web framework and routing</td></tr>
      <tr><td>HTML / CSS</td><td>Frontend structure and styling</td></tr>
      <tr><td>JavaScript</td><td>Client-side interactivity</td></tr>
      <tr><td>SQL Server</td><td>Relational database and persistence</td></tr>
      <tr><td>SignalR</td><td>Real-time WebSocket communication (support chat)</td></tr>
    </tbody>
  </table>
</section>

<!-- Contributing -->
<section>
  <p class="section-label">Contributing</p>
  <p class="section-title">How to contribute</p>
  <div class="steps">
    <div class="step">
      <span class="step-num">1</span>
      <div class="step-body">
        <p class="step-title">Fork the project</p>
      </div>
    </div>
    <div class="step">
      <span class="step-num">2</span>
      <div class="step-body">
        <p class="step-title">Create a feature branch</p>
        <p class="step-desc"><span class="code-inline">git checkout -b feature/your-feature</span></p>
      </div>
    </div>
    <div class="step">
      <span class="step-num">3</span>
      <div class="step-body">
        <p class="step-title">Commit your changes</p>
        <p class="step-desc"><span class="code-inline">git commit -m 'Add your feature'</span></p>
      </div>
    </div>
    <div class="step">
      <span class="step-num">4</span>
      <div class="step-body">
        <p class="step-title">Push and open a pull request</p>
        <p class="step-desc"><span class="code-inline">git push origin feature/your-feature</span></p>
      </div>
    </div>
  </div>
</section>

<!-- Author -->
<div class="author-strip">
  <div>
    <p class="author-name">Ehab Okasha</p>
    <p class="author-sub">Author &amp; maintainer</p>
  </div>
  <a class="gh-link" href="https://github.com/EhabOkashahh" target="_blank">
    <svg width="14" height="14" viewBox="0 0 16 16" fill="currentColor"><path d="M8 0C3.58 0 0 3.58 0 8c0 3.54 2.29 6.53 5.47 7.59.4.07.55-.17.55-.38 0-.19-.01-.82-.01-1.49-2.01.37-2.53-.49-2.69-.94-.09-.23-.48-.94-.82-1.13-.28-.15-.68-.52-.01-.53.63-.01 1.08.58 1.23.82.72 1.21 1.87.87 2.33.66.07-.52.28-.87.51-1.07-1.78-.2-3.64-.89-3.64-3.95 0-.87.31-1.59.82-2.15-.08-.2-.36-1.02.08-2.12 0 0 .67-.21 2.2.82.64-.18 1.32-.27 2-.27.68 0 1.36.09 2 .27 1.53-1.04 2.2-.82 2.2-.82.44 1.1.16 1.92.08 2.12.51.56.82 1.27.82 2.15 0 3.07-1.87 3.75-3.65 3.95.29.25.54.73.54 1.48 0 1.07-.01 1.93-.01 2.
