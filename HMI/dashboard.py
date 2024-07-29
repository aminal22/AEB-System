import sys
import subprocess
import random
from PyQt5.QtCore import *
from PyQt5.QtGui import *
from PyQt5.QtWidgets import *

# Define painter render hints for better quality rendering
_RENDER_HINTS = (
    QPainter.RenderHint.Antialiasing
    | QPainter.RenderHint.HighQualityAntialiasing
    | QPainter.RenderHint.SmoothPixmapTransform
    | QPainter.RenderHint.LosslessImageRendering
    | QPainter.RenderHint.NonCosmeticDefaultPen
    | QPainter.RenderHint.TextAntialiasing
)

class DashBoardMain(QWidget):
    """Main dashboard class with start and loading screens."""
    
    def __init__(self):
        super().__init__()
        self.setWindowFlags(Qt.WindowType.Tool | Qt.WindowType.FramelessWindowHint)
        self.setAttribute(Qt.WidgetAttribute.WA_TranslucentBackground, True)
        self.setFixedSize(1280, 720)
        self.initUI()

    def initUI(self):
        self.stacked_widget = QStackedWidget(self)
        self.stacked_widget.setContentsMargins(0, 0, 0, 0)
        self.stacked_widget.setFixedSize(self.size())
        self.start_screen()
        self.loading_screen()
        self.stacked_widget.setCurrentIndex(0)

    def start_screen(self):
        start_widget = QWidget()
        start_widget.setFixedSize(self.size())
        start_widget.setStyleSheet("background-color: #2E2E2E;")
        self.stacked_widget.addWidget(start_widget)

        start_button = QPushButton("Start", start_widget)
        start_button.setFixedSize(self.width() // 5, self.width() // 5)
        start_button.move(self.rect().center() - start_button.rect().center())
        start_button.setStyleSheet("""
            QPushButton {
                background-color: qlineargradient(spread:pad, x1:0, y1:0, x2:1, y2:0, stop:0 #f00, stop:0.5 #f80, stop:1.0 #f00);
                color: white; border-radius: %spx;
            }
            QPushButton::hover {
                background-color: qlineargradient(spread:pad, x1:0, y1:0, x2:1, y2:0, stop:0 #0f0, stop:0.5 #5f0, stop:1.0 #0f0);
            }
        """ % (self.width() // 10))
        start_button.clicked.connect(self.start_button_action)

    def start_button_action(self):
        self.stacked_widget.setCurrentIndex(1)
        self.loading_screen_start()

    def loading_screen(self):
        loading_screen_widget = QWidget()
        loading_screen_widget.setContentsMargins(0, 0, 0, 0)
        loading_screen_widget.setFixedSize(self.width(), self.height())
        loading_screen_widget.setStyleSheet("background-color: #1C1C1C;")  # Background for loading screen
        self.stacked_widget.addWidget(loading_screen_widget)

        get_ready_label = QLabel(loading_screen_widget)
        get_ready_label.setFixedSize(int(self.width() * 0.6), int(self.height() * 0.2))
        get_ready_label.move(self.rect().center() - get_ready_label.rect().center() - QPoint(0, get_ready_label.height()))
        get_ready_label.setStyleSheet("background-color: rgba(0, 0, 0, 0); color: rgb(207, 184, 29)")
        get_ready_label.setAlignment(Qt.AlignmentFlag.AlignCenter)

        saftey_rule_font = QFont("Consolas", 0, 0, True)
        saftey_rule_font.setBold(True)
        saftey_rule_font.setPixelSize(round(self.width() * 0.035))
        get_ready_label.setFont(saftey_rule_font)
        get_ready_label.setText("Get ready for the ride...")

        self.loading_progress_bar = QProgressBar(loading_screen_widget)
        self.loading_progress_bar.setContentsMargins(0, 0, 0, 0)
        self.loading_progress_bar.setFixedSize(int(loading_screen_widget.width() * 0.7), int(loading_screen_widget.height() * 0.1))
        self.loading_progress_bar.move(self.rect().center() - self.loading_progress_bar.rect().center())
        self.loading_progress_bar.setAlignment(Qt.AlignmentFlag.AlignCenter)

        loding_progress_bar_font = QFont("Consolas", 0, 0, True)
        loding_progress_bar_font.setBold(True)
        loding_progress_bar_font.setPixelSize(round(self.width() * 0.04))
        self.loading_progress_bar.setFont(loding_progress_bar_font)

        grad = "qlineargradient(spread:pad, x1:0, y1:0, x2:1, y2:0, stop:0 {color1}, stop:{value} {color2}, stop: 1.0 {color3});".format(
            color1=QColor(240, 0, 0).name(), color2=QColor(255, 80, 0).name(), color3=QColor(255, 255, 0).name(), value=0.3)
        self.loading_progress_bar.setStyleSheet("QProgressBar {background-color: rgba(0, 0, 0, 0); color: white; border-radius: %spx;}" % str(self.loading_progress_bar.height() // 2)
            + "QProgressBar::chunk {background-color: %s; border-radius: %spx;}" % (grad, str(self.loading_progress_bar.height() // 2)))

        self.progress_bar_animation = QPropertyAnimation(self.loading_progress_bar, b"value")
        self.progress_bar_animation.setStartValue(self.loading_progress_bar.height() * 0.2)
        self.progress_bar_animation.valueChanged.connect(self.update_safety_rule)
        self.progress_bar_animation.setEndValue(100)
        self.progress_bar_animation.setDuration(3000)

        self.saftey_rules = ("Do not drink and drive.", "Always wear a helmet!", "Drive within the speed limits.",
                             "Don't use mobile phones while driving.", "Buckle up before you drive.", "Keep a safe distance from vehicles!")

        self.saftey_rule_label = QLabel(loading_screen_widget)
        self.saftey_rule_label.setFixedSize(int(self.width() * 0.6), int(self.height() * 0.2))
        self.saftey_rule_label.move(self.rect().center() - self.saftey_rule_label.rect().center() + QPoint(0, self.saftey_rule_label.height()))
        self.saftey_rule_label.setStyleSheet("background-color: rgba(0, 0, 0, 0); color: yellow")
        self.saftey_rule_label.setAlignment(Qt.AlignmentFlag.AlignCenter)

        saftey_rule_font = QFont("Consolas", 0, 0, True)
        saftey_rule_font.setBold(True)
        saftey_rule_font.setPixelSize(round(self.width() * 0.025))
        self.saftey_rule_label.setFont(saftey_rule_font)
        self.saftey_rule_label.setText(random.sample(self.saftey_rules, 1)[0])

    def update_safety_rule(self, value):
        if value % 33 == 0:
            self.saftey_rule_label.setText(random.choice(self.saftey_rules))

    def loading_screen_start(self):
        self.loading_progress_bar.setValue(0)
        self.progress_animation = QPropertyAnimation(self.loading_progress_bar, b"value")
        self.progress_animation.setDuration(3000)
        self.progress_animation.setStartValue(0)
        self.progress_animation.setEndValue(100)
        self.progress_animation.valueChanged.connect(self.update_safety_rule)
        self.progress_animation.finished.connect(self.launch_csharp_application)
        self.progress_animation.finished.connect(self.hide_dashboard)  # Hide dashboard when finished
        self.progress_animation.start()

    def hide_dashboard(self):
        self.hide()  # Hide the dashboard window

    def launch_csharp_application(self):
        try:
            subprocess.Popen(["C:\\Users\\hp\\source\\repos\\AEB2\\AEB\\bin\\Debug\\AEB.exe"])  # Update the path to your C# app
        except Exception as e:
            QMessageBox.critical(self, "Error", f"Failed to start C# application: {str(e)}")

def main():
    app = QApplication(sys.argv)
    window = DashBoardMain()
    window.show()
    sys.exit(app.exec())

if __name__ == "__main__":
    main()
