import React from "react";

import {
  FormControl,
  InputLabel,
  MenuItem,
  Select,
  TextField,
  Grid,
  Button,
} from "@mui/material";
import { useForm } from "react-hook-form";

import { useUsers } from "@/app/hooks/useUsers";

export const UserFilter: React.FC = () => {
  const { applyFilter } = useUsers();
  const { register, handleSubmit } = useForm<SearchUsersQuery>();

  const handleFilter = (data: SearchUsersQuery) => {
    applyFilter(data);
  };

  return (
    <form onSubmit={handleSubmit(handleFilter)}>
      <Grid container spacing={2} alignItems="center">
        <Grid item xs={12} md={6} lg={4}>
          <TextField
            fullWidth
            type="text"
            placeholder="Filtrar por nome"
            {...register("name")}
            label="Nome"
            variant="standard"
          />
        </Grid>
        <Grid item xs={12} md={6} lg={4}>
          <TextField
            fullWidth
            type="text"
            placeholder="Filtrar por email"
            {...register("email")}
            label="Email"
            variant="standard"
          />
        </Grid>
        <Grid item xs={12} md={3} lg={2}>
          <FormControl fullWidth>
            <InputLabel>Ativo</InputLabel>
            <Select fullWidth label="Ativo" {...register("active")}>
              <MenuItem>--Nenhum--</MenuItem>
              <MenuItem value="false">Não</MenuItem>
              <MenuItem value="true">Sim</MenuItem>
            </Select>
          </FormControl>
        </Grid>

        <Grid item xs={12} md={3} lg={2} container justifyContent="flex-end">
          <Button type="submit" variant="contained" fullWidth>
            Filtrar
          </Button>
        </Grid>
      </Grid>
    </form>
  );
};
