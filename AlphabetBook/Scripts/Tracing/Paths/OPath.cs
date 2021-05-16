
namespace AlphabetBook
{
    public class OPath : PlayerTracing
    {


        protected override void EndLine()
        {
            base.EndLine();

            if(index == 0)
            {
                isPathCompleted = CheckPath(9, 13);

                if (isPathCompleted)
                    CompletedTracing();
            }
        }

       

    }// end class

}

