
namespace AlphabetBook
{
    public class ZPath : PlayerTracing
    {

        protected override void EndLine()
        {
            base.EndLine();


            if(index == 0)
            {
                isPathCompleted = CheckPath(10, 14);

                if (isPathCompleted)
                    CompletedTracing();
            }

        }

    }

}

