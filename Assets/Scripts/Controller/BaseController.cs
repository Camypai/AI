using Ig.Model;

namespace Ig.Controller
{
    public abstract class BaseController
    {
        protected bool Enabled { get; private set; }

        public virtual void On()
        {
            On(null);
        }

        public virtual void On(BaseModel baseModel)
        {
            Enabled = true;
        }

        public virtual void Off()
        {
            Enabled = false;
        }

        public virtual void Toggle()
        {
            if (!Enabled)
            {
                On();
            }
            else
            {
                Off();
            }
        }
    }
}