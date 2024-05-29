using LitJson;


namespace TimelineRuntimeExtension
{
    public abstract class TimelineCmdBase
    {
        public int Id;
        public double StartTime;
        public double EndTime;
        public double DurationTime;
        public double CurrentTime;

        public TimelineBase Owner;
        public bool IsStart;
        public bool IsEnd;

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
            Id = id;
            StartTime = startTime;
            EndTime = endTime;
            DurationTime = durationTime;
            CurrentTime = 0f;
            IsStart = false;
            IsEnd = false;
        }

        public void Update(double deltaTime)
        {
            OnUpdate(deltaTime);
        }

        public void Start()
        {
            IsStart = true;
            OnStart();
        }

        public void End()
        {
            IsEnd = true;
            OnEnd();
        }

        public abstract void OnStart();

        public abstract void OnUpdate(double deltaTime);

        public abstract void OnEnd();
    }

}