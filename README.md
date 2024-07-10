# Smart AEB system 


## Objective

The objective of this project is to develop an advanced driver assistance system (ADAS) that integrates various detection and communication technologies to enhance vehicle safety and efficiency. The system is designed to detect objects (pedestrians, vehicles, etc.), calculate distances, display warnings, and, if necessary, apply braking to avoid collisions. This project aims to leverage state-of-the-art technologies in embedded systems and V2X (Vehicle-to-Everything) communication.

## Technologies Used

- **YOLO (You Only Look Once):** Chosen for its ability to perform real-time object detection with high accuracy. YOLO frames object detection as a regression problem to spatially separated bounding boxes and associated class probabilities. It processes images quickly, making it ideal for real-time road safety applications.
- **Qt for HMI Development:** Selected for developing our Human-Machine Interface (HMI) due to its flexibility and capabilities in creating intuitive and efficient user interfaces. Qt supports multi-platform deployment, ensuring a consistent user experience.

## Key Components

### Image Processing
- **Objective:** Crucial for detecting and identifying objects such as pedestrians, vehicles, and traffic signs in real-time.
- **Implementation:** Developed Python scripts to process images captured by the camera. The YOLO algorithm was used for object detection due to its speed and accuracy.
  - **Reference:** https://github.com/RmdanJr/vehicle-distance-estimation

### Real-Time Implementation
- **Objective:** Ensure the system operates in real-time to provide immediate feedback and actions.
- **Implementation:** Connected a camera to the model to send images for processing and verify real-time performance.

### Development of the Human-Machine Interface (HMI)
- **Objective:** Create an intuitive and efficient interface for users to interact with the system.
- **Implementation:**
  - Installed Visual Studio and Qt dependencies.
  - Installed Qt and configured it as the Qt builder.
  - Used PyQt and other libraries installed via pip for development.
  - Designed the HMI with components to display warnings, connect to the serial port, and communicate with other ECUs via the CAN bus.

### Data Integration via CAN Bus
- **Objective:** Facilitate communication between the main control units (MCUs) via the CAN bus.
- **Implementation:** Configured the CAN bus communication between the MCUs to ensure reliable data transfer and system coordination.


## Conclusion

This project represents a significant step towards safer and smarter vehicles by utilizing the latest technologies in embedded systems and communication. The success of this project is based on careful design, rigorous technology selection, and a deep understanding of end-user needs.

## How to Run the Project

1. **Clone the Repository:**
   ```bash
   git clone https://github.com/aminal22/AEBS.git
   cd AEBS
