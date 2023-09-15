import React, { CSSProperties } from "react";

import styles from "./Button.module.css";

type ButtonPropsType = React.ButtonHTMLAttributes<HTMLButtonElement> & {
  placeholder:
    | "Entrar"
    | "Cadastrar"
    | "Recuperar"
    | "Alterar senha"
    | "Confirmar";
  onClick?: () => void;
  containerStyle?: CSSProperties;
  inputStyle?: CSSProperties;
};

export const Button: React.FC<ButtonPropsType> = ({
  placeholder,
  onClick,
  ...props
}) => (
  <button className={styles.button} onClick={onClick} {...props}>
    {placeholder}
  </button>
);

const ButtonContainer: React.FC<ButtonPropsType> = ({
  placeholder,
  onClick,
  containerStyle,
}) => (
  <div style={containerStyle}>
    <Button placeholder={placeholder} onClick={onClick} />
  </div>
);

export default ButtonContainer;
