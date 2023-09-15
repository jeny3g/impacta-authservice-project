import { RegisterOptions } from "react-hook-form";

type ValidationMessages = Record<string, RegisterOptions>;

export const validationMessages: ValidationMessages = {
  name: {
    required: "Nome é obrigatório",
    maxLength: { value: 50, message: "Nome deve ter no máximo 50 caracteres" },
    pattern: {
      value: /^[\p{L}\s'-]+$/u,
      message: "Nome deve conter apenas letras",
    },
  },
  email: {
    required: "E-mail é obrigatório",
    pattern: {
      value: /^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$/i,
      message: "E-mail inválido",
    },
  },
  password: {
    required: "Senha é obrigatória",
    minLength: { value: 8, message: "Senha deve ter no mínimo 8 caracteres" },
    pattern: {
      value: /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]/,
      message:
        "Senha deve conter pelo menos uma letra maiúscula, uma letra minúscula, um número e um caractere especial",
    },
  },
  confirmPassword: {
    required: "Senha é obrigatória",
    minLength: { value: 8, message: "Senha deve ter no mínimo 8 caracteres" },
    pattern: {
      value: /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]/,
      message: "As senhas precisam ser iguais",
    },
  },
};
