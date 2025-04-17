using System.Collections.Generic;
using DevIONETEC.Business.Notificacoes;

namespace DevIONETEC.Business.Intefaces
{
    public interface INotificador
    {
        bool TemNotificacao();
        List<Notificacao> ObterNotificacoes();
        void Handle(Notificacao notificacao);
    }
}