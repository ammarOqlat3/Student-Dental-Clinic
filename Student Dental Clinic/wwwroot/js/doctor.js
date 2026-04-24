    const COLORS = [
    'linear-gradient(135deg,#6366f1,#8b5cf6)',
    'linear-gradient(135deg,#f59e0b,#ef4444)',
    'linear-gradient(135deg,#0ea5e9,#22c55e)',
    'linear-gradient(135deg,#ec4899,#f43f5e)',
    'linear-gradient(135deg,#14b8a6,#0ea5e9)',
    'linear-gradient(135deg,#84cc16,#10b981)',
    'linear-gradient(135deg,#f97316,#eab308)',
    'linear-gradient(135deg,#a855f7,#3b82f6)',
];

//const patients = @Html.Raw(Json.Serialize(patients));
//const students = @Html.Raw(Json.Serialize(students));

   
    const reports = [
    {id:1, student:'Ahmad Salem',     patient:'Sara Hassan',   caseType:'Molar Filling',         date:'April 11, 2025', description:'Molar filling successfully performed using composite material.',               grade:17,   maxGrade:20, status:'graded',  color:COLORS[0] },
    {id:2, student:'Lina Abdullah',   patient:'Faisal Nour',   caseType:'Wisdom Tooth Extraction',date:'April 9, 2025',  description:'Extraction performed correctly with proper post-extraction instructions.',    grade:16,   maxGrade:20, status:'graded',  color:COLORS[3] },
    {id:3, student:'Reem Al-Zahrani', patient:'Nour Al-Din',   caseType:'Orthodontics',           date:'April 8, 2025',  description:'Report covers the first orthodontic session and wire placement.',             grade:null, maxGrade:20, status:'pending', color:COLORS[1] },
    {id:4, student:'Dana Al-Omari',   patient:'Layla Khair',   caseType:'Dental Bridge',          date:'April 7, 2025',  description:'Abutment teeth prepared and accurate impression taken.',                       grade:18,   maxGrade:20, status:'graded',  color:COLORS[7] },
    {id:5, student:'Salma Hassan',    patient:'Rashid Saad',   caseType:'Cleaning & Polishing',   date:'April 6, 2025',  description:'Full cleaning with tartar removal and oral hygiene instructions provided.',   grade:null, maxGrade:20, status:'pending', color:COLORS[5] },
    ];

    let selectedStudentForAssign = null;
    let currentReportId = null;
    let currentPatientForAssign = null;
    let currentFilterStatus = 'all';

    function showView(name) {
        document.querySelectorAll('.db-view').forEach(v => v.classList.remove('active'));
    document.getElementById('view-' + name).classList.add('active');
          document.querySelectorAll('.db-nav-link').forEach(l => {
        l.classList.toggle('active', l.getAttribute('onclick')?.includes("'" + name + "'"));
          });
    const titles = {dashboard:'Dashboard', profile:'Profile', patients:'Patients', students:'Students', reports:'Reports', settings:'Settings' };
    document.getElementById('pageTitle').textContent = titles[name] || '';
    if (name === 'patients')  renderPatients();
    if (name === 'students')  renderStudents();
    if (name === 'reports')   renderReports();
    if (name === 'dashboard') renderDashboardPerf();
        }

    function renderDashboardPerf() {
          const list = document.getElementById('studentPerformanceList');
          const sorted = [...students].sort((a, b) => (b.avg || 0) - (a.avg || 0));
          list.innerHTML = sorted.map(s => `
    <div class="d-flex align-items-center gap-2 mb-3">
        <div style="width:32px;height:32px;border-radius:50%;background:${s.color};display:flex;align-items:center;justify-content:center;color:white;font-weight:700;font-size:12px;flex-shrink:0;">${s.name[0]}</div>
        <div style="flex:1;">
            <div class="d-flex justify-content-between mb-1">
                <span style="font-size:12px;font-weight:600;">${s.name}</span>
                <span style="font-size:12px;font-weight:700;color:var(--primary);">${s.avg ? s.avg + '/20' : '—'}</span>
            </div>
            <div class="db-progress">
                <div class="db-progress-fill" style="width:${s.avg ? (s.avg / 20 * 100) : 0}%"></div>
            </div>
        </div>
    </div>
    `).join('');
        }

    function renderPatients(filter = 'all', search = '') {
          const statusMap   = {pending:'db-badge-pending', assigned:'db-badge-assigned', done:'db-badge-done', inprogress:'db-badge-inprogress' };
    const statusLabel = {pending:'Awaiting Assignment', assigned:'Assigned', done:'Completed', inprogress:'In Progress' };
    let data = patients;
          if (filter !== 'all') data = data.filter(p => p.status === filter);
          if (search) data = data.filter(p => p.name.toLowerCase().includes(search.toLowerCase()) || p.case.toLowerCase().includes(search.toLowerCase()));
          document.getElementById('patientsTbody').innerHTML = data.map((p, i) => `
    <tr>
        <td style="color:var(--text-muted);">${i + 1}</td>
        <td>
            <div class="d-flex align-items-center gap-2">
                <div class="db-avatar" style="background:${COLORS[i % COLORS.length]};">${p.name[0]}</div>
                <span style="font-weight:600;">${p.name}</span>
            </div>
        </td>
        <td>${p.age} / ${p.gender}</td>
        <td style="color:var(--text-muted);">${p.phone}</td>
        <td>${p.case}</td>
        <td style="color:var(--text-muted);">${p.date}</td>
        <td><span class="db-badge-status ${statusMap[p.status]}">${statusLabel[p.status]}</span></td>
        <td>${p.student ? `<span style="font-weight:600;color:var(--primary);">${p.student}</span>` : '<span style="color:#ccc;">—</span>'}</td>
        <td>
            <div class="d-flex gap-1">
                ${p.status === 'pending' ? `<button class="db-btn-icon" style="background:#e0f2fe;color:#0284c7;" onclick="openAssign('${p.name}','${p.case}')"><i class="fa-solid fa-user-plus"></i></button>` : ''}
                <button class="db-btn-icon" style="background:#f7fafc;color:#6c8799;"><i class="fa-solid fa-eye"></i></button>
            </div>
        </td>
    </tr>
    `).join('');
        }

    function filterPatients(f, btn) {
        currentFilterStatus = f;
          document.querySelectorAll('#view-patients .db-filter-tab').forEach(b => b.classList.remove('active'));
    btn.classList.add('active');
    renderPatients(f);
        }
    function searchPatients(v) {renderPatients(currentFilterStatus, v); }

    function renderStudents() {
        document.getElementById('studentsGrid').innerHTML = students.map(s => `
            <div class="col-md-3 col-sm-6">
              <div class="db-card" style="cursor:pointer;transition:transform .2s;"
                   onmouseover="this.style.transform='translateY(-3px)'" onmouseout="this.style.transform=''"
                   onclick="openStudentModal(${s.id})">
                <div class="text-center p-3 border-bottom">
                  <div style="width:60px;height:60px;border-radius:50%;background:${s.color};margin:0 auto 10px;display:flex;align-items:center;justify-content:center;color:white;font-size:22px;font-weight:800;">${s.name[0]}</div>
                  <div style="font-size:14px;font-weight:700;">${s.name}</div>
                  <div style="font-size:11px;color:var(--text-muted);margin-top:2px;">${s.number}</div>
                  <span class="badge bg-primary bg-opacity-10 text-primary mt-2" style="font-size:11px;">GPA: ${s.gpa}</span>
                </div>
                <div class="p-3">
                  <div class="d-flex justify-content-between mb-1" style="font-size:12px;"><span style="color:var(--text-muted);">Cases</span><span style="font-weight:700;color:var(--primary);">${s.casesCount}</span></div>
                  <div class="d-flex justify-content-between mb-1" style="font-size:12px;"><span style="color:var(--text-muted);">Graded</span><span style="font-weight:700;color:var(--success);">${s.graded}</span></div>
                  <div class="d-flex justify-content-between" style="font-size:12px;"><span style="color:var(--text-muted);">Grade Average</span><span style="font-weight:700;color:var(--primary);">${s.avg ? s.avg + '/20' : '—'}</span></div>
                </div>
                <div class="p-2 pt-0">
                  <button class="db-btn-outline w-100" onclick="event.stopPropagation();openStudentModal(${s.id})"><i class="fa-solid fa-eye me-1"></i>View Details</button>
                </div>
              </div>
            </div>
          `).join('');
        }

    function openStudentModal(id) {
          const s = students.find(x => x.id === id);
    document.getElementById('studentModalBody').innerHTML = `
    <div class="db-student-profile">
        <div class="big-avatar" style="background:${s.color};">${s.name[0]}</div>
        <div class="s-info">
            <h5>${s.name}</h5>
            <p>${s.number} · Year ${s.year}</p>
            <p class="mt-1"><i class="fa-solid fa-star text-warning me-1"></i>GPA: ${s.gpa}</p>
        </div>
    </div>
    <div class="db-detail-grid mb-3">
        <div class="db-detail-item"><div class="d-label">Phone</div><div class="d-val">${s.phone}</div></div>
        <div class="db-detail-item"><div class="d-label">Email</div><div class="d-val" style="font-size:12px;">${s.email}</div></div>
        <div class="db-detail-item"><div class="d-label">Cases</div><div class="d-val">${s.casesCount} cases</div></div>
        <div class="db-detail-item"><div class="d-label">Graded</div><div class="d-val">${s.graded} cases</div></div>
        <div class="db-detail-item"><div class="d-label">Grade Average</div><div class="d-val">${s.avg ? s.avg + '/20' : 'Not graded'}</div></div>
        <div class="db-detail-item"><div class="d-label">Academic Year</div><div class="d-val">${s.year}</div></div>
    </div>
    <h6 style="font-size:13px;font-weight:700;margin-bottom:10px;"><i class="fa-solid fa-list-check me-2 text-primary"></i>Assigned Cases</h6>
    ${s.cases.length
        ? s.cases.map(c => `
                  <div class="db-case-item">
                    <div style="width:28px;height:28px;border-radius:7px;background:var(--bg);display:flex;align-items:center;justify-content:center;color:var(--primary);"><i class="fa-solid fa-tooth" style="font-size:12px;"></i></div>
                    <div>
                      <div class="c-name">${c.split(' - ')[0]}</div>
                      <div class="c-patient">Patient: ${c.split(' - ')[1] || '—'}</div>
                    </div>
                    <span class="db-badge-status db-badge-assigned ms-auto">Assigned</span>
                  </div>
                `).join('')
        : `<div class="text-center py-3" style="color:var(--text-muted);font-size:13px;"><i class="fa-solid fa-inbox fa-2x d-block mb-2"></i>No cases assigned yet</div>`
    }
    `;
    document.getElementById('studentModal').classList.add('open');
        }

    function renderReports(filter = 'all') {
        let data = reports;
          if (filter !== 'all') data = data.filter(r => r.status === filter);
    const bgColors = ['#e0f2fe','#fdf4ff','#f0fdf4','#fff7ed','#fefce8'];
          document.getElementById('reportsGrid').innerHTML = data.map((r, i) => `
    <div class="col-md-4">
        <div class="db-report-card">
            <div class="db-report-images">
                <div class="db-img-slot" style="background:${bgColors[i % bgColors.length]};">
                    <div class="text-center" style="color:var(--text-muted);"><i class="fa-solid fa-image fa-lg d-block mb-1"></i><span style="font-size:10px;font-weight:600;">Before Treatment</span></div>
                    <span class="db-img-label">Before</span>
                </div>
                <div class="db-img-slot" style="background:${bgColors[(i+1) % bgColors.length]};border-left:2px solid white;">
                    <div class="text-center" style="color:var(--text-muted);"><i class="fa-solid fa-image fa-lg d-block mb-1"></i><span style="font-size:10px;font-weight:600;">After Treatment</span></div>
                    <span class="db-img-label">After</span>
                </div>
            </div>
            <div class="db-report-body">
                <div class="r-title">${r.caseType}</div>
                <div class="r-meta"><i class="fa-solid fa-user-graduate me-1"></i>${r.student} · <i class="fa-solid fa-calendar me-1 ms-1"></i>${r.date}</div>
                <div style="font-size:11px;color:#444;margin-top:6px;line-height:1.6;">${r.description.substring(0, 90)}...</div>
            </div>
            <div class="db-report-footer">
                <div>
                    <div style="font-size:10px;color:var(--text-muted);margin-bottom:2px;">Patient</div>
                    <div style="font-size:13px;font-weight:600;">${r.patient}</div>
                </div>
                ${r.grade !== null
                    ? `<div class="db-grade-circle">${r.grade}</div>`
                    : `<button class="db-btn-primary" style="font-size:11px;" onclick="openGradeModal(${r.id})"><i class="fa-solid fa-star me-1"></i>Grade</button>`
                }
            </div>
        </div>
    </div>
    `).join('');
        }

    function filterReports(f, btn) {
        document.querySelectorAll('#view-reports .db-filter-tab').forEach(b => b.classList.remove('active'));
    btn.classList.add('active');
    renderReports(f);
        }

    function openGradeModal(reportId) {
        currentReportId = reportId;
          const r = reports.find(x => x.id === reportId);
    document.getElementById('gradeReportInfo').innerHTML = `
    <div class="d-flex gap-2 align-items-center p-2 rounded" style="background:var(--bg);">
        <div style="width:36px;height:36px;border-radius:8px;background:linear-gradient(135deg,var(--primary),var(--accent));display:flex;align-items:center;justify-content:center;color:white;flex-shrink:0;"><i class="fa-solid fa-file-medical" style="font-size:14px;"></i></div>
        <div>
            <div style="font-size:13px;font-weight:700;">${r.caseType}</div>
            <div style="font-size:11px;color:var(--text-muted);">Student: ${r.student} · Patient: ${r.patient}</div>
        </div>
    </div>
    `;
    const slider = document.getElementById('gradeSlider');
    const initVal = Math.floor(r.maxGrade * 0.75);
    slider.max = r.maxGrade;
    slider.value = initVal;
    document.getElementById('gradePreview').textContent = initVal;
    document.getElementById('gradeOutOf').textContent = 'out of ' + r.maxGrade;
    document.getElementById('gradeMaxLabel').textContent = r.maxGrade;
    document.getElementById('gradeModal').classList.add('open');
        }

    function updateGradePreview(v) {document.getElementById('gradePreview').textContent = v; }

    function confirmGrade() {
          const grade = parseInt(document.getElementById('gradeSlider').value);
          const r = reports.find(x => x.id === currentReportId);
    if (r) {r.grade = grade; r.status = 'graded'; }
    closeModal('gradeModal');
    renderReports();
    showToast(`Grade ${grade}/${r.maxGrade} saved for ${r.student}`, 'fa-star');
        }

    function openAssign(patientName, caseType) {
        currentPatientForAssign = patientName;
    selectedStudentForAssign = null;
    document.getElementById('assignPatientInfo').innerHTML = `
    <h6><i class="fa-solid fa-hospital-user me-2"></i>${patientName}</h6>
    <div style="font-size:12px;color:var(--text-muted);"><i class="fa-solid fa-tooth me-1"></i>Case type: <strong style="color:var(--text-dark);">${caseType}</strong></div>
    `;
          document.getElementById('assignStudentList').innerHTML = students.map(s => `
    <div class="db-student-select-item" id="ssel-${s.id}" onclick="selectStudentForAssign(${s.id})">
        <div class="db-avatar" style="background:${s.color};width:36px;height:36px;">${s.name[0]}</div>
        <div style="flex:1;">
            <div class="s-name">${s.name}</div>
            <div class="s-info">${s.casesCount} assigned cases · GPA: ${s.gpa}</div>
        </div>
        <div style="width:18px;height:18px;border-radius:50%;border:2px solid #e2e8f0;display:flex;align-items:center;justify-content:center;" id="schk-${s.id}"></div>
    </div>
    `).join('');
    document.getElementById('assignModal').classList.add('open');
        }

    function selectStudentForAssign(id) {
        selectedStudentForAssign = id;
          document.querySelectorAll('.db-student-select-item').forEach(el => el.classList.remove('selected'));
          document.querySelectorAll('[id^="schk-"]').forEach(el => {el.style.background = ''; el.innerHTML = ''; el.style.borderColor = '#e2e8f0'; });
    document.getElementById('ssel-' + id).classList.add('selected');
    const chk = document.getElementById('schk-' + id);
    chk.style.background  = 'var(--primary)';
    chk.style.borderColor = 'var(--primary)';
    chk.innerHTML = '<i class="fa-solid fa-check" style="color:white;font-size:9px;"></i>';
        }

    function confirmAssign() {
          if (!selectedStudentForAssign) {showToast('Please select a student', 'fa-exclamation'); return; }
          const s = students.find(x => x.id === selectedStudentForAssign);
          const p = patients.find(x => x.name === currentPatientForAssign);
    if (p) {p.status = 'assigned'; p.student = s.name; }
    s.casesCount++;
    s.cases.push(p ? p.case + ' - ' + p.name : '');
    closeModal('assignModal');
    renderPatients(currentFilterStatus);
    renderDashboardPerf();
    showToast(`Case assigned to ${s.name}`, 'fa-check');
        }

    function closeModal(id) {document.getElementById(id).classList.remove('open'); }

        document.querySelectorAll('.db-modal-overlay').forEach(m => {
        m.addEventListener('click', function (e) { if (e.target === this) this.classList.remove('open'); });
        });

    function showToast(msg, icon = 'fa-check') {
          const container = document.getElementById('toastContainer');
    const toast = document.createElement('div');
    toast.className = 'db-toast-item';
    toast.innerHTML = `<i class="fa-solid ${icon}" style="color:var(--accent);"></i>${msg}`;
    container.appendChild(toast);
          setTimeout(() => {
        toast.style.opacity = '0'; toast.style.transition = 'opacity .3s';
            setTimeout(() => toast.remove(), 300);
          }, 3000);
        }

renderDashboardPerf();


function openAssignModal(id) {
    document.getElementById("assignModal").style.display = "flex";

    // نحط رقم المريض بالـ hidden input
    document.getElementById("patientIdInput").value = id;
}
function openEditModal() {
    var modal = new bootstrap.Modal(document.getElementById('editModal'));
    modal.show();
}
