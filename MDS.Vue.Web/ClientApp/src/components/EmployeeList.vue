<template>
    <h4 style="margin-bottom:20px;">
        {{employeeList.departmentName}}
    </h4>
    <p>
        <strong style="margin-right:10px;">Сотрудников:</strong> {{employeeList.employeesCount}}
    </p>
    <p>
        <strong style="margin-right:10px;">Руководитель:</strong>
        {{employeeList.headName}} ({{employeeList.headPosition}})
    </p>
    <p>
        <button @click="reload" class="btn btn btn-primary">Обновить</button>
        <button @click="create" class="btn btn btn-success" style="margin-left: 10px">Создать</button>
        <button @click="back" class="btn btn btn-secondary" style="float:right;">Назад к списку отделов</button>
    </p>
    <table class='table table-striped'>
        <thead>
            <tr>
                <th>Сотрудник</th>
                <th>Должность</th>
                <th></th>
            </tr>
        </thead>
        <tbody>
            <tr v-for="employee of employeeList.employees" v-bind:key="employee">
                <td>
                    {{ employee.name }}
                </td>
                <td>
                    {{ employee.position }}
                </td>
                <td width="250" style="text-align:right;">
                    <button @click="edit(employee.id)" class="btn btn-info">Изменить</button>
                    <button  @click="remove(employee)" class="btn btn-danger" style="margin-left:10px">Удалить</button>
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
        name: "EmployeeList",
        data() {
            return {
                departmentId: null,
                employeeList: {
                    directionId: null,
                    departmentName: null,
                    employeesCount: null,
                    employees: []
                }
            }
        },
        methods: {
            getEmployes() {

                axios.get('/api/list/employee?id=' + this.departmentId)
                    .then((response) => {
                        this.employeeList = response.data;
                    })
                    .catch(function (error) {
                        alert(error);
                    });
            },
            reload() {
                this.getEmployes();
            },
            create() {
                router.push({
                    name: 'createEmployee',
                    params: {
                        departmentId: this.departmentId
                    }
                });
            },
            edit(id) {
                router.push({
                    name: 'editEmployee',
                    params: {
                        id: id
                    }
                });
            },
            back() {
                router.push({ name: 'departments', params: { id: this.employeeList.directionId } });
            },
            remove(employee) {

                if (!confirm("Вы действительно хотите удалить данные сотрудника [" + employee.name + "]?"))
                    return;

                axios.delete('/api/OrgUnits/' + employee.id)
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

            this.departmentId = route.params.id;

            this.getEmployes();
        }
    }
</script>