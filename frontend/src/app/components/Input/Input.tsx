import React, { ReactElement, memo } from "react";

import styles from "./Input.module.css";
import Tooltip from "../Tooltip/Tooltip";

type InputProps = React.InputHTMLAttributes<HTMLInputElement> & {
  leftIcon?: ReactElement;
  rightIcon?: ReactElement;
  containerStyle?: React.CSSProperties;
  inputStyle?: React.CSSProperties;
  error?: boolean;
  errorMessage?: string;
};

export const Input = memo(
  React.forwardRef<HTMLInputElement, InputProps>(
    (
      {
        leftIcon,
        rightIcon,
        containerStyle,
        style,
        errorMessage,
        error,
        ...props
      },
      ref,
    ) => {
      return (
        <div
          data-testid="input-container"
          className={error ? styles.inputError : styles.container}
          style={{ ...containerStyle }}
        >
          <div
            className={error ? styles.leftIconError : styles.icon}
            aria-label="Left Icon"
          >
            {leftIcon}
          </div>

          <input ref={ref} className={styles.input} style={style} {...props} />

          {error && (
            <Tooltip message={errorMessage}>
              <div className={styles.rightIcon} aria-label="Right Icon">
                {rightIcon}
              </div>
            </Tooltip>
          )}
        </div>
      );
    },
  ),
);

Input.displayName = "Input";
