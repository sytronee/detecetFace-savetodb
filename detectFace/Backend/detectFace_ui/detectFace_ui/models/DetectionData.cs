namespace detectFace_ui.models
{
    public class DetectionData
    {
        public string label { get; set; }
        public double confidence { get; set; }
        public bundybox bbox { get; set; }
        public string camera_id { get; set; }
    }
}
