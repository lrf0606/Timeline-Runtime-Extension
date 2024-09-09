using LitJson;


namespace TimelineRuntimeExtension
{
    public abstract class TimelineCmdBase
    {
        public int m_ID;
        public double m_StartTime;
        public double m_EndTime;
        public double m_DurationTime;
        public double m_CurrentTime;

        public TimelineBase m_Owner;
        public bool m_IsStart;
        public bool m_IsEnd;

        public void LoadJsonData(JsonData jsonData)
        {
            var Id = (int)jsonData["Guid"];
            var startTime = (double)jsonData["StartTime"];
            var endTime = (double)jsonData["EndTime"];
            var durationTime = (double)jsonData["DurationTime"];
            foreach(JsonData fieldJsonData in jsonData["Fields"])
            {
                var fieldName = (string)fieldJsonData["FieldName"];
                var fieldType = (string)fieldJsonData["FieldType"];
                var fieldValue = (string)fieldJsonData["FieldValue"];
                ParseField(fieldName, fieldType, fieldValue);
            }
            Initlization(Id, startTime, endTime, durationTime);
        }

        public virtual void ParseField(string fieldName, string fieldType, string fieldValue)
        {

        }

        public void Initlization(int id, double startTime, double endTime, double durationTime)
        {
            m_ID = id;
            m_StartTime = startTime;
            m_EndTime = endTime;
            m_DurationTime = durationTime;
            m_CurrentTime = 0f;
            m_IsStart = false;
            m_IsEnd = false;
        }

        public void Update(double deltaTime)
        {
            OnUpdate(deltaTime);
        }

        public void Start()
        {
            m_IsStart = true;
            OnStart();
        }

        public void End()
        {
            m_IsEnd = true;
            OnEnd();
        }

        public abstract void OnStart();

        public abstract void OnUpdate(double deltaTime);

        public abstract void OnEnd();
    }

}