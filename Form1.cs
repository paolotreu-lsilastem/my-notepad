namespace MyNotepad;

public partial class Form1 : Form
{
    private string? currentFilePath = null;
    private bool isDirty = false;
    private TextBox textBox = null!;

    public Form1(string? filePath = null)
    {
        InitializeComponent();
        this.Icon = new Icon("notepad-icon.ico");
        InitializeNotepadUI();
        if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
        {
            textBox.Text = File.ReadAllText(filePath!);
            currentFilePath = filePath;
            isDirty = false;
            this.Text = $"{Path.GetFileName(filePath)} - MyNotepad";
        }
        else
        {
            this.Text = "Untitled - MyNotepad";
        }
    }

    private void InitializeNotepadUI()
    {
        // Set up the main text box
        textBox = new TextBox();
        textBox.Multiline = true;
        textBox.Dock = DockStyle.Fill;
        textBox.Font = new Font("Consolas", 11);
        textBox.ScrollBars = ScrollBars.Both;
        textBox.AcceptsTab = true;
        textBox.AcceptsReturn = true;
        textBox.WordWrap = false;
        textBox.TextChanged += (s, e) => { isDirty = true; };
        this.Controls.Add(textBox);

        // Set up the menu
        var menu = new MenuStrip();
        var fileMenu = new ToolStripMenuItem("File");
        var newItem = new ToolStripMenuItem("New", null, NewFile);
        var openItem = new ToolStripMenuItem("Open...", null, OpenFile);
        var saveItem = new ToolStripMenuItem("Save", null, SaveFile);
        var saveAsItem = new ToolStripMenuItem("Save As...", null, SaveAsFile);
        var exitItem = new ToolStripMenuItem("Exit", null, (s, e) => this.Close());
        fileMenu.DropDownItems.AddRange(new ToolStripItem[] { newItem, openItem, saveItem, saveAsItem, new ToolStripSeparator(), exitItem });
        menu.Items.Add(fileMenu);
        this.MainMenuStrip = menu;
        this.Controls.Add(menu);
        menu.Dock = DockStyle.Top;

        // open window centered
        this.StartPosition = FormStartPosition.CenterScreen;
    }

    private void NewFile(object? sender, EventArgs e)
    {
        if (PromptSaveIfDirty())
        {
            textBox.Clear();
            currentFilePath = null;
            isDirty = false;
            this.Text = "Untitled - MyNotepad";
        }
    }

    private void OpenFile(object? sender, EventArgs e)
    {
        if (!PromptSaveIfDirty()) return;
        using var ofd = new OpenFileDialog();
        ofd.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
        if (ofd.ShowDialog() == DialogResult.OK)
        {
            textBox.Text = File.ReadAllText(ofd.FileName);
            currentFilePath = ofd.FileName;
            isDirty = false;
            this.Text = $"{Path.GetFileName(currentFilePath)} - MyNotepad";
        }
    }

    private void SaveFile(object? sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(currentFilePath))
        {
            SaveAsFile(sender, e);
        }
        else
        {
            File.WriteAllText(currentFilePath, textBox.Text);
            isDirty = false;
        }
    }

    private void SaveAsFile(object? sender, EventArgs e)
    {
        using var sfd = new SaveFileDialog();
        sfd.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
        if (sfd.ShowDialog() == DialogResult.OK)
        {
            File.WriteAllText(sfd.FileName, textBox.Text);
            currentFilePath = sfd.FileName;
            isDirty = false;
            this.Text = $"{Path.GetFileName(currentFilePath)} - MyNotepad";
        }
    }

    private bool PromptSaveIfDirty()
    {
        if (!isDirty) return true;
        var result = MessageBox.Show("Do you want to save changes?", "MyNotepad", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
        if (result == DialogResult.Yes)
        {
            SaveFile(this, EventArgs.Empty);
            return !isDirty;
        }
        return result == DialogResult.No;
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        if (!PromptSaveIfDirty())
        {
            e.Cancel = true;
        }
        base.OnFormClosing(e);
    }
}
