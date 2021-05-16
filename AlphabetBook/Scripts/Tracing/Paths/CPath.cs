
namespace AlphabetBook
{
    public class CPath : PlayerTracing
    {

        protected override void EndLine()
        {
            base.EndLine();

            if (index == 0)
            {
                isPathCompleted = CheckPath(10, 13);

                if (isPathCompleted)
                    CompletedTracing();
            }
        }


    }

}

