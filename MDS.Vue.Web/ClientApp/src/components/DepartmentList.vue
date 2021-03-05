<template>
    <h4 style="margin-bottom:20px;">
        {{departmentList.directionName}}
    </h4>
    <p>
        <strong style="margin-right:10px;">Отделов:</strong> {{departmentList.departmentsCount}}
    </p>
    <p>
        <button @click="reload" class="btn btn btn-primary">Обновить</button>
        <button @click="create" class="btn btn btn-success" style="margin-left: 10px">Создать</button>
        <button @click="back" class="btn btn btn-secondary" style="float:right;">Назад к списку управлений</button>
    </p>
    <table class='table table-striped'>
        <thead>
            <tr>
                <th>Отдел</th>
                <th>Руководитель</th>
                <th style="text-align:right;">Сотрудников</th>
                <th></th>
            </tr>
        </thead>
        <tbody>
            <tr v-for="department of departmentList.departments" v-bind:key="department">
                <td>
                    <router-link :to="{ name: 'employees', params : { id: department.id } }"> {{ department.name }}</router-link>
                </td>
                <td>
                    {{ department.headName }}
                </td>
                <td style="text-align:right;">
                    {{ department.employeesCount }}
                </td>
                <td width="250" style="text-align:right;">
                    <button @click="edit(department.id)" class="btn btn-info">Изменить</button>
                    <button @click="remove(department)" class="btn btn-danger" style="margin-left:10px">Удалить</button>
                </td>
            </tr>
        </tbody>
    </table>
</template>


<script>
    import axios from 'axios'
    import { useRoute } from 'vue-router'
    import router from '../router'
    export default {
        name: "DepartmentList",
        data() {
            return {
                directionId: null,
                departmentList: {
                    directionName: null,
                    departmentsCount: null,
                    departments: []
                }
            }
        },
        methods: {
            getDepartments() {

                axios.get('/api/list/department?id=' + this.directionId)
                    .then((response) => {
                        this.departmentList = response.data;
                    })
                    .catch(function (error) {
                        alert(error);
                    });
            },
            create() {
                router.push({
                    name: 'createDepartment',
                    params: {
                        directionId: this.directionId
                    }
                });
            },
            edit(id) {
                router.push({
                    name: 'editDepartment',
                    params: {
                        id: id
                    }
                });
            },
            reload() {
                this.getDepartments();
            },
            back() {
                router.push({ name: 'directions' });
            },
            remove(department) {

                if (!confirm("Вы действительно хотите удалить отдел [" + department.name + "]?"))
                    return;

                axios.delete('/api/OrgUnits/' + department.id)
                    .then(() => {
                        this.reload();
                    })
                    .catch(function (error) {
                        alert(error);
                    });
            }
        },
        mounted() {

            const route = useRoute()

            this.directionId = route.params.id;

            this.getDepartments();
        }
    }
</script>