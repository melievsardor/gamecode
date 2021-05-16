

namespace AlphabetBook
{
    public class SPathUz : PlayerTracing
    {

        protected override void EndLine()
        {
            base.EndLine();

            if (index == 0)
            {
                isPathCompleted = CheckPath(10, 17);

                if (isPathCompleted)
                    CompletedTracing();
            }
        }


    }
}


